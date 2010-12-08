#region Using Directives

using System.Windows;

using Example.Core.Resources.Contracts.Tasks;
using Example.Core.Tasks.RichClient;

#endregion

namespace Example.RichClient
{
    public partial class MainPage
    {
        private readonly IMessageTasks messageTasks;

        public MainPage()
        {
            InitializeComponent();

            // pretend we're doing IoC
            this.messageTasks = new MessageTasks();
        }

        private void OnShowMessageButtonClick(object sender, RoutedEventArgs e)
        {
            this.textBlock.Text = this.messageTasks.DisplayMessage();
        }
    }
}
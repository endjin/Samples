namespace Example.Core.Tasks.RichClient
{
    using Example.Core.Resources;
    using Example.Core.Resources.Contracts.Tasks;

    public class MessageTasks : IMessageTasks
    {
        public string DisplayMessage()
        {
            return new MessageResource().Message;
        }
    }
}
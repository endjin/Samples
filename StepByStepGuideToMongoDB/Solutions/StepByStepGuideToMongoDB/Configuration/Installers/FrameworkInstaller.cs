namespace StepByStepGuideToMongoDB.Configuration.Installers
{
    #region Using Directives

    using System.ComponentModel.Composition;

    using Castle.MicroKernel.Registration;

    using Endjin.Core.Windsor.Installers.Convention;

    #endregion

    [Export(typeof(IWindsorInstaller))]
    public class FrameworkInstaller : FrameworkInstallerBase<FrameworkInstaller>
    {
    }
}
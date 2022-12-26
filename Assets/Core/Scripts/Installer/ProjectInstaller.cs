using Zenject;

namespace Core
{
    /// <summary>
    /// Bindings for application lifetime.
    /// </summary>
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ILevelManager), typeof(IInitializable)).To<LevelManager>().AsSingle().NonLazy();
        }
    }
}
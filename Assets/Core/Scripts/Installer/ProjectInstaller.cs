using Zenject;

namespace Core
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ILevelManager), typeof(IInitializable)).To<LevelManager>().AsSingle().NonLazy();
        }
    }
}
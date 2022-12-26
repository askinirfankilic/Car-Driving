using Core;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ILevelManager>().To<LevelManager>().AsSingle().NonLazy();
    }
}
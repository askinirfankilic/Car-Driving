using Core;
using UnityEngine;
using Zenject;

/// <summary>
/// Bindings for scene lifetime.
/// </summary>
public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EventManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
    }
}
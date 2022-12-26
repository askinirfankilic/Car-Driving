using Game.Scripts.Data;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CarSettingsInstaller", menuName = "Installers/CarSettingsInstaller")]
public class CarSettingsInstaller : ScriptableObjectInstaller<CarSettingsInstaller>
{
    [SerializeField]
    private CarSettings _carSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_carSettings);
    }
}
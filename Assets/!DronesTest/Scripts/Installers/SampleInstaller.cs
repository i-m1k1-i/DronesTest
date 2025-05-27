using DronesTest.Gameplay;
using Zenject;
using UnityEngine;

public class SampleInstaller : MonoInstaller
{
    [SerializeField] private Resource resource;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ResourcePool>()
            .AsSingle()
            .WithArguments(resource);
        Container.BindInterfacesAndSelfTo<ResourceManager>()
            .AsSingle()
            .NonLazy();
    }
}
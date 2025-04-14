using UnityEngine;
using VContainer;
using VContainer.Unity;

public class AudioLifeTimeScope : LifetimeScope
{
    [SerializeField] private AudioSystem _audioSystem;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_audioSystem).As<AudioSystem>();
        builder.RegisterComponentInHierarchy<MainMenu>();
    }
}

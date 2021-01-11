using Zenject;

namespace Eyedrops.Scripts.Input
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class KeyInputInstaller : Installer<KeyInputInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IInputEventProvider>()
                .To<KeyInputEventProvider>()
                .FromNewComponentOnNewGameObject()
                .AsCached();
        }
    }
}
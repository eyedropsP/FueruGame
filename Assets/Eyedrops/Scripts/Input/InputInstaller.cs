using Zenject;

namespace Eyedrops.Scripts.Input
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            KeyInputInstaller.Install(Container);
        }
    }
}
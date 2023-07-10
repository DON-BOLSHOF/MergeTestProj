using BoardElements;
using Zenject;

namespace Installers
{
    public class BoardItemGeneratorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BoardItemGenerator>().AsSingle();
        }
    }
}
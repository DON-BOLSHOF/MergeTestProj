using GridElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GridConverterInstaller : MonoInstaller
    {
        [SerializeField] private Grid _grid;
        
        public override void InstallBindings()
        {
            Container.Bind<GridConverter>().FromInstance(new GridConverter(_grid)).AsSingle();
        }
    }
}
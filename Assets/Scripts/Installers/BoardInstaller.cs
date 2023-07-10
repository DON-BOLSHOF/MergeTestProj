using BoardElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BoardInstaller : MonoInstaller
    {
        [SerializeField] private Board _instance; 
        
        public override void InstallBindings()
        {
            Container.Bind<Board>().FromInstance(_instance).AsSingle();
        }
    }
}
using BoardElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BoardSlotFactoryInstaller : MonoInstaller
    {
        [SerializeField] private BoardSlot _instance;
        
        public override void InstallBindings()
        {
            Container.BindFactory<BoardSlot, BoardSlot.BoardSlotFactory>().FromComponentInNewPrefab(_instance)
                .AsSingle();
        }
    }
}
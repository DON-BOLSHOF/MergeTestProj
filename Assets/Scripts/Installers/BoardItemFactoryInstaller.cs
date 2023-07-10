using BoardElements;
using GridElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BoardItemFactoryInstaller : MonoInstaller
    {
        [SerializeField] private BoardItem _instance;
        
        public override void InstallBindings()
        {
            Container.BindFactory<BoardItem, BoardItem.BoardItemFactory>().FromComponentInNewPrefab(_instance)
                .AsSingle();
        }
    }
}
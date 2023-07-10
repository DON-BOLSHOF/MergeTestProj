using Merge;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MergeSystemInstaller : MonoInstaller
    {
        [SerializeField] private MergeSystem _instance;
        
        public override void InstallBindings()
        {
            Container.Bind<MergeSystem>().FromInstance(_instance).AsSingle();
        }
    }
}
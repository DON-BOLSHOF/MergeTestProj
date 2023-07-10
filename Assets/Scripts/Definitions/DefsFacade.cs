using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "DefsFacade", menuName = "Defs/DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private MergeChainsDefs _mergeChainsDefs;
        [SerializeField] private MergeItemDefs _mergeItemDefs;
        [SerializeField] private GamePlayDefs _gamePlayDefs;

        public MergeChainsDefs MergeChainsDefs => _mergeChainsDefs;
        public MergeItemDefs MergeItemDefs => _mergeItemDefs;
        public GamePlayDefs GamePlayDefs => _gamePlayDefs;
        
        private static DefsFacade _instance;

        public static DefsFacade I => _instance == null ? LoadDefs() : _instance; //На мой взгляд, тут синглтон оправдан в силу того что
                                                                                  //это статический неизменяемый конфиг.   
                                                                                  
        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("Definition/DefsFacade");
        }
    }
}
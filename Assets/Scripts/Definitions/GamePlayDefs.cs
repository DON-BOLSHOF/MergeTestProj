using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "GamePlayDefs", menuName = "Defs/GamePlayDefs")]
    public class GamePlayDefs : ScriptableObject
    {
        [SerializeField] private int _modifierToMerge = 3;
        [SerializeField] private int _modifierToSuperMerge = 6;
        [SerializeField] private int _countToSuperMerge = 5;

        public int ModifierToMerge => _modifierToMerge;
        public int ModifierToSuperMerge => _modifierToSuperMerge;
        public int CountToSuperMerge => _countToSuperMerge;
    }
}
using System;
using System.Collections.Generic;
using Merge.MergeItems;
using UnityEngine;

namespace Merge
{
    [CreateAssetMenu(fileName = "MergeInstruction", menuName = "Defs/MergeInstruction")]
    public class MergeChain : ScriptableObject//Не нравится рассинхронизированность MergeItems и MergeChains
    {
        [SerializeField] private string _instructionId;
        [SerializeField] private List<Stage> _stages;

        public string InstructionId => _instructionId;
        public List<Stage> Stages => _stages;
    }

    [Serializable]
    public class Stage
    {
        [SerializeField] private string _id;
        [SerializeField] private MergeItem _mergeItem;

        public string Id => _id;
        public MergeItem MergeItem => _mergeItem;
    }
}
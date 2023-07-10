using System.Collections.Generic;
using BoardElements;
using Definitions;
using Merge.MergeItems;
using UnityEngine;

namespace Utils
{
    public static class MergeUtils
    {
        public static MergeItem GetNextStageItem(MergeItem item)
        {
            var chain = DefsFacade.I.MergeChainsDefs.GetChainWithItem(item);
            var stageIndex = chain.Stages.FindIndex(_ => _.MergeItem.GetType() == item.GetType());
            
            return stageIndex>=chain.Stages.Count-1 ? null : chain.Stages[stageIndex + 1].MergeItem;
        }
        
        public static bool IsPossibleToMerge(BoardSlot boardSlot, MergeItem mergeItem)
        {
            var isEmptySlot = boardSlot.BoardItem == null;
            var isTypesEqual = !isEmptySlot && boardSlot.BoardItem.MergeItem.GetType() == mergeItem.GetType();
            var isMergeToItself = !isEmptySlot && boardSlot.BoardItem.MergeItem.Equals(mergeItem);
            var hasUpLevelToMerge = GetNextStageItem(mergeItem) != null;

            return isTypesEqual && !isMergeToItself && hasUpLevelToMerge;
        }

        public static int GetSlotsAmountToMerge(int slotsCount)
        {
            var modifierToMerge = DefsFacade.I.GamePlayDefs.ModifierToMerge;
            var modifierToSuperMerge = DefsFacade.I.GamePlayDefs.ModifierToSuperMerge;
            var countToSuperMerge = DefsFacade.I.GamePlayDefs.CountToSuperMerge;
            
            
            var emergedCount = slotsCount / modifierToMerge;

            if (slotsCount % modifierToSuperMerge == countToSuperMerge)
                emergedCount++;

            return emergedCount;
        }

        public static int GetSlotsAmountToRemain(int slotsCount)
        {
            var modifierToMerge = DefsFacade.I.GamePlayDefs.ModifierToMerge;
            var modifierToSuperMerge = DefsFacade.I.GamePlayDefs.ModifierToSuperMerge;
            var countToSuperMerge = DefsFacade.I.GamePlayDefs.CountToSuperMerge;
            
            var remainSlots = slotsCount % modifierToMerge;

            return slotsCount % modifierToSuperMerge == countToSuperMerge ? 0 : remainSlots;
        }

        public static bool TryFindDropSlotPosition(List<List<BoardSlot>> matrix, BoardSlot boardSlot, MergeItem item,out Vector2Int result)
        {
            result = default;
            return MatrixUtils<BoardSlot>.TryFindIndexOfObject(matrix, boardSlot, out result);
        }

        public static bool TryFindDraggingSlotPosition(List<List<BoardSlot>> matrix, MergeItem item, out Vector2Int itemPosition)
        {
            return MatrixUtils<BoardSlot>.TryFindIndexWithCondition(matrix, slot =>
                BoardItemUtils.IsMergeItemBelongsToBoardItem(slot.BoardItem, item), out itemPosition);
        }
    }
}
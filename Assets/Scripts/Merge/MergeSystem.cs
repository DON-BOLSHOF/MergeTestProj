using System.Collections.Generic;
using System.Linq;
using BoardElements;
using Definitions;
using Merge.MergeItems;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Merge
{
    public class MergeSystem : MonoBehaviour
    {
        [Inject] private Board _board;

        private MergeHints _mergeHints = new MergeHints();

        public ReactiveCommand<BoardSlot> OnMergedSlots = new ReactiveCommand<BoardSlot>(); 

        public void Start()
        {
            _board.OnPotentialMergeItemEnterSlot += RevealThePotentialHints;
            _board.IsMerging += TryMerge;
            OnMergedSlots.Subscribe(slot => _mergeHints.OnMerged(slot))
                .AddTo(this)
                ;
        }

        private void RevealThePotentialHints(BoardSlot slot, MergeItem item)
        {
            _mergeHints.DeactivateCurrentMergeHints();
            
            if (!TryFindSlotsToMerge(slot, item, out var positions) ||
                positions.Count<DefsFacade.I.GamePlayDefs.ModifierToMerge) return;

            if (MergeUtils.TryFindDraggingSlotPosition(_board.BoardMatrix, item, out var itemPosition))
                positions = new Queue<Vector2Int>(positions.Where(position => !itemPosition.Equals(position)));
            
            var slots = positions.Select(position =>
                _board.BoardMatrix[position.x][position.y]).ToList();

            _mergeHints.ActivatePotentialMergeHints(slots);
        }

        private bool TryMerge(BoardSlot slot, MergeItem item)
        {
            if (!TryFindSlotsToMerge(slot, item, out var slotsToMerge) ||
                slotsToMerge.Count<DefsFacade.I.GamePlayDefs.ModifierToMerge) return false;

            var nextItem = MergeUtils.GetNextStageItem(item);

            var slotsCount = slotsToMerge.Count;
                
            EmergeNextHierarchySlots(slotsToMerge, nextItem, slotsCount);
            EmergeRemainSlots(slotsToMerge, DefsFacade.I.MergeItemDefs.Get($"{item.Id}"), slotsCount);
            DisappearSuperfluousItems(slotsToMerge);

            return true;
        }

        private void EmergeNextHierarchySlots(Queue<Vector2Int> slotsToMerge, MergeItem nextItem, int slotsCount)
        {
            var emergedCount = MergeUtils.GetSlotsAmountToMerge(slotsCount);

            for (int i = 0; i < emergedCount; i++)
            {
                var position = slotsToMerge.Dequeue();

                _board.BoardMatrix[position.x][position.y].EmergeItemTo(nextItem);

                OnMergedSlots?.Execute(_board.BoardMatrix[position.x][position.y]);
            }
        }

        private void EmergeRemainSlots(Queue<Vector2Int> slotsToMerge, MergeItem item, int slotsCount)
        {
            var remainedCount = MergeUtils.GetSlotsAmountToRemain(slotsCount);
            
            for (int i = 0; i < remainedCount; i++)
            {
                var position = slotsToMerge.Dequeue();

                _board.BoardMatrix[position.x][position.y].EmergeItemTo(item);

                OnMergedSlots?.Execute(_board.BoardMatrix[position.x][position.y]);
            }
        }

        private void DisappearSuperfluousItems(Queue<Vector2Int> slotsToMerge)
        {
            while (slotsToMerge.Count > 0)
            {
                var position = slotsToMerge.Dequeue();

                _board.BoardMatrix[position.x][position.y].DisappearItem();
            }
        }

        private bool TryFindSlotsToMerge(BoardSlot boardSlot, MergeItem item, out Queue<Vector2Int> result)
        {
            if (!MergeUtils.IsPossibleToMerge(boardSlot, item)
                || !MergeUtils.TryFindDropSlotPosition(_board.BoardMatrix, boardSlot, item, out var slotPosition))
            {
                result = null;
                return false;
            }

            result = new Queue<Vector2Int>();

            MergeStep(item, slotPosition, result);

            if (MergeUtils.TryFindDraggingSlotPosition(_board.BoardMatrix, item, out var itemPosition))
                result.Enqueue(itemPosition);

            return true;
        }

        private void MergeStep(MergeItem item, Vector2Int position, Queue<Vector2Int> positionsToMerge)
        {
            if (positionsToMerge.Contains(position) ||
                !MatrixUtils<BoardSlot>.IsPositionInMatrix(_board.BoardMatrix, position))
                return;

            if (MergeUtils.IsPossibleToMerge(_board.BoardMatrix[position.x][position.y], item))
            {
                positionsToMerge.Enqueue(position);

                MergeStep(item, new Vector2Int(position.x + 1, position.y), positionsToMerge);
                MergeStep(item, new Vector2Int(position.x - 1, position.y), positionsToMerge);
                MergeStep(item, new Vector2Int(position.x, position.y + 1), positionsToMerge);
                MergeStep(item, new Vector2Int(position.x, position.y - 1), positionsToMerge);
            }
        }

        ~MergeSystem()
        {
            _board.OnPotentialMergeItemEnterSlot -= RevealThePotentialHints;
            _board.IsMerging -= TryMerge;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Merge.MergeItems;
using UniRx;
using UnityEngine;
using Utils;

namespace BoardElements
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private BoardCreator _boardCreator;
        [SerializeField] private BoardFiller _boardFiller;

        private List<List<BoardSlot>> _boardMatrix = new List<List<BoardSlot>>();

        public List<List<BoardSlot>> BoardMatrix => _boardMatrix;

        public Action<BoardSlot, MergeItem> OnPotentialMergeItemEnterSlot;
        public Func<BoardSlot, MergeItem, bool> IsMerging;

        private void Start()
        {
            _boardMatrix = _boardCreator.GenerateEmptySlotsGrid();
            _boardFiller.FulfilGrid(_boardMatrix);

            foreach (var boardSlot in _boardMatrix.SelectMany(boardSlots => boardSlots))
            {
                boardSlot.OnMergeItemEntered
                    .Subscribe(item => { OnPotentialMergeItemEnterSlot?.Invoke(boardSlot, item); })
                    .AddTo(this)
                    ;

                boardSlot.OnDropped
                    .Subscribe(item =>
                    {
                        if (!IsMerging.Invoke(boardSlot, item))
                            ReloadSlot(boardSlot, item);
                    })
                    .AddTo(this)
                    ;
            }
        }

        private void ReloadSlot(BoardSlot boardSlot, MergeItem item) //Расплети потом макаронину
        {
            var isSlotEmpty = TryPutAwayBoardItem(boardSlot.BoardItem);
            var isSlotExchanged = TryExchangeSlotByBoardItem(boardSlot, item, isSlotEmpty);
            
            if (!isSlotExchanged && !isSlotEmpty)
            {
                boardSlot.ReloadSlot(item);
            }
        }

        private bool TryPutAwayBoardItem(BoardItem boardItem)
        {
            if (boardItem != null)
            {
                var foundEmptySlot = MatrixUtils<BoardSlot>.FindObjectWithCondition(_boardMatrix,
                    slot => slot.BoardItem == null);

                if (foundEmptySlot == null) return false;

                foundEmptySlot.SetData(boardItem);
            }

            return true;
        }

        private bool TryExchangeSlotByBoardItem(BoardSlot boardSlot, MergeItem item, bool isSlotEmpty)
        {
            if (MatrixUtils<BoardSlot>.TryFindObjectWithCondition(_boardMatrix,
                    slot => BoardItemUtils.IsMergeItemBelongsToBoardItem(slot.BoardItem, item),
                    out var foundBoardSlotWithItem))
            {
                var currentItem = boardSlot.BoardItem;
                boardSlot.SetData(foundBoardSlotWithItem.BoardItem);

                if (!isSlotEmpty)
                    foundBoardSlotWithItem.SetData(currentItem);
                else
                    foundBoardSlotWithItem.Dispose();

                return true;
            }

            return false;
        }
    }
}
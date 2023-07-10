using Merge.MergeItems;
using UnityEngine;
using Zenject;

namespace BoardElements
{
    public class BoardSlot : MergeItemDetector
    {
        [Inject] private BoardItemGenerator _boardItemGenerator;//Это лишь обертка над настоящим MergeItem-ом,
                                                                //создание обертки думаю можно доверить итему. 
        
        private BoardItem _boardItem;
        public BoardItem BoardItem => _boardItem;

        public void SetData(BoardItem boardItem)
        {
            _boardItem = boardItem;

            _boardItem.transform.parent = this.transform;
            _boardItem.transform.localPosition = Vector3.zero;
        }

        public void ReloadSlot(MergeItem item)
        {
            var boardItem = _boardItemGenerator.Generate(item);
            SetData(boardItem);
        }

        public void EmergeItemTo(MergeItem nextItem)
        {
            _boardItem.EmergeByCall(() =>
            {
                Destroy(_boardItem.gameObject);
                ReloadSlot(nextItem);
            });
        }

        public void DisappearItem()
        {
            _boardItem.Disappear(() =>
            {
                Destroy(_boardItem.gameObject);
                Dispose();
            });
        }

        public void ActivateGlowingHint()
        {
            _boardItem.ActivateGlowingHint();
        }

        public void DeactivateGlowingHint()
        {
            _boardItem.DeactivateGlowingHint();
        }

        public void Dispose()
        {
            _boardItem = null;
        }

        public class BoardSlotFactory : PlaceholderFactory<BoardSlot>
        {
        }
    }
}
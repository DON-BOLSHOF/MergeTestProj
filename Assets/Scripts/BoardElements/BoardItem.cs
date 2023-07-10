using System;
using CodeAnimations;
using GridElements;
using Merge.MergeItems;
using UniRx;
using UnityEngine;
using Zenject;

namespace BoardElements
{
    public class BoardItem : MonoBehaviour
    {
        [SerializeField] private GlowingHint _glowingHint;

        [Inject] private GridConverter _gridConverter;

        private MergeItem _mergeItem;
        public MergeItem MergeItem => _mergeItem;

        public void Init(MergeItem item)
        {
            _mergeItem = item;
            
            Subscribe();
        }

        private void Subscribe()
        {
            _mergeItem.OnBeginDragCommand
                .Subscribe(_ => ActivateGlowingHint())
                .AddTo(this);

            _mergeItem.OnDragCommand
                .Subscribe(worldPosition =>  RelocateHint(worldPosition))
                .AddTo(this)
                ;

            _mergeItem.OnEndDragCommand
                .Subscribe(_ =>
                {
                    DropItem();
                    DeactivateGlowingHint();
                })
                .AddTo(this)
                ;
        }

        public void ActivateGlowingHint()
        {
            _glowingHint.StartGlow();
        }

        private void RelocateHint(Vector3 position)
        {
            var neededPos = _gridConverter.ConvertToCellWorldPosition(position);
            neededPos.y -= 2.2f; //Глянь потом, что с гридом, почему он съехал!

            _glowingHint.MoveTo(neededPos);
        }

        private void DropItem()
        {
            transform.localPosition = Vector3.zero;
            _glowingHint.transform.localPosition = Vector3.zero;
            _mergeItem.transform.localPosition = Vector3.zero;
        }

        public void DeactivateGlowingHint()
        {
            _glowingHint.Fade();
        }

        public void EmergeByCall(Action ExchangeCall)
        {
            _mergeItem.Disappear(() =>
            {
                ExchangeCall();
                _mergeItem.Appear();
            });
        }

        public void Disappear(Action OnDisapper = null)
        {
            _mergeItem.Disappear(OnDisapper);
        }

        public class BoardItemFactory : PlaceholderFactory<BoardItem>
        {
            
        }
    }
}
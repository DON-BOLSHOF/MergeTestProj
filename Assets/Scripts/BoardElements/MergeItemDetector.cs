using Merge.MergeItems;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BoardElements
{
    public class MergeItemDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        public ReactiveCommand<MergeItem> OnDropped = new ReactiveCommand<MergeItem>();
        public ReactiveCommand<MergeItem> OnMergeItemEntered = new ReactiveCommand<MergeItem>();
        public ReactiveCommand<MergeItem> OnMergeItemExit = new ReactiveCommand<MergeItem>();
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent<MergeItem>(out var mergeItem))
            {
                OnMergeItemEntered?.Execute(mergeItem);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent<MergeItem>(out var mergeItem))
            {
                OnMergeItemExit?.Execute(mergeItem);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<MergeItem>(out var mergeItem))
            {
                OnDropped?.Execute(mergeItem);
            }
        }
    }
}
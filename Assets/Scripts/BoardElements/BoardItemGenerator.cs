using Merge.MergeItems;
using UnityEngine;
using Zenject;

namespace BoardElements
{
    public class BoardItemGenerator
    {
        [Inject] private BoardItem.BoardItemFactory _factory;

        public BoardItem Generate(MergeItem mergeItem)
        {
            var instance = _factory.Create();

            var mergeItemInstance = Object.Instantiate(mergeItem, instance.transform);
            
            instance.Init(mergeItemInstance);

            return instance;
        } 
    }
}
using System.Collections.Generic;
using System.Linq;
using Definitions;
using UnityEngine;

namespace BoardElements
{
    public class BoardFiller : MonoBehaviour
    {
        [SerializeField] private int _maximumInstances;
        [SerializeField, Range(0,1f)] private float _chanceToSpawnObject;
        
        public void FulfilGrid(List<List<BoardSlot>> boardMatrix)//Чисто прототипный спавн, при надобности усложним.
        {
            var itemsCount = 0;
            
            var items = DefsFacade.I.MergeItemDefs.GetAllItems();

            foreach (var boardSlot in boardMatrix.SelectMany(boardSlots => boardSlots))
            {
                if(_maximumInstances<= itemsCount) return;

                var chance = Random.Range(0f, 1f);

                if (chance <= _chanceToSpawnObject)
                {
                    boardSlot.ReloadSlot(items[Random.Range(0, items.Count)]);
                    itemsCount++;
                }
            }
        }
    }
}
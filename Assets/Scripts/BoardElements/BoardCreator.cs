using System.Collections.Generic;
using Definitions;
using UnityEngine;
using Zenject;

namespace BoardElements
{
    public class BoardCreator : MonoBehaviour
    {
        [Inject] private BoardSlot.BoardSlotFactory _factory;
        
        [SerializeField] private int gridHeight = 5;
        [SerializeField] private int gridWidth = 5;
        [SerializeField] private float tileSize = 2f;

        public List<List<BoardSlot>> GenerateEmptySlotsGrid()
        {
            var tiles = new  List<List<BoardSlot>>();
            for(int x=0; x< gridHeight; x++)
            {
                tiles.Add(new List<BoardSlot>());
                for (int y = 0; y < gridWidth; y++)
                {
                    BoardSlot newTile = _factory.Create();
                    newTile.transform.parent = this.transform;
                    float posX = -22.1f + (x * tileSize + y * tileSize) / 2f; 
                    float posY =  13f + (x * tileSize - y * tileSize) / 3.4f; 
                    newTile.transform.position = new Vector2(posX, posY);
                    newTile.name = x + ", " + y;
                    tiles[x].Add(newTile);
                }
            }

            return tiles;
        }
    }
}

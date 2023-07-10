using UnityEngine;

namespace GridElements
{
    public class GridConverter
    {
        private Grid _grid;

        public GridConverter(Grid grid)
        {
            _grid = grid;
        }

        public Vector3 ConvertToCellWorldPosition(Vector3 position)
        {
            var gridPosition = _grid.WorldToCell(position);
            var neededPos = _grid.GetCellCenterWorld(gridPosition);

            return neededPos;
        }
    }
}
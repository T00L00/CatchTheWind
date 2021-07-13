using UnityEngine;

namespace CTW.Wind
{
    /// <summary>
    /// Single cell that contains a world position and grid coordinate
    /// </summary>
    public class Cell
    {
        public Vector2 worldPos;
        public Vector2Int gridIndex;

        public Cell(Vector2 _worldPos, Vector2Int _gridIndex)
        {
            worldPos = _worldPos;
            gridIndex = _gridIndex;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTW.Wind
{
    /// <summary>
    /// Class used to construct grid data structure based on scene dimensions. Actual grid is not drawn on scene. 
    /// </summary>
    public class Grid
    {
        // 2D array of cells
        public Cell[,] grid { get; private set; }

        // grid dimensions: x rows by y columns
        public Vector2Int gridSize { get; private set; }

        // Where in scene to start building grid
        public Vector2 origin { get; set; }

        // Cell spacing
        public float cellDiameter { get; set; }
        private float cellRadius;

        public Grid(float _cellDiameter, Vector2Int _gridSize)
        {
            cellDiameter = _cellDiameter;
            cellRadius = cellDiameter / 2f;
            gridSize = _gridSize;
        }

        /// <summary>
        /// Generate grid with user-input parameters
        /// </summary>
        public void CreateGrid()
        {
            grid = new Cell[gridSize.x, gridSize.y];

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2 worldPos = new Vector2(origin.x + (cellDiameter * x + cellRadius), origin.y + (cellDiameter * y + cellRadius));
                    grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monobehavior to draw vector field. Toggleable.
/// </summary>
public class DebugField : MonoBehaviour
{
    public VectorFieldController controller;
    public bool toggle = false;

    // Desired cell size
    public float spacing;

    // Desired grid dimensions (x rows by y columns)
    public Vector2Int gridSize;

    // Grid start location in world space
    public Vector2 origin;

    private void OnDrawGizmos()
    {
        Grid sceneGrid = new Grid(spacing, gridSize);
        sceneGrid.origin = origin;
        sceneGrid.CreateGrid();
        if (toggle)
        {
            Vector2 force;
            Vector2 pos;
            for (int i = 0; i < sceneGrid.gridSize.x; i++)
            {
                for (int j = 0; j < sceneGrid.gridSize.y; j++)
                {
                    pos = sceneGrid.grid[i, j].worldPos;
                    force = VectorField.WindCurrentForce(controller.vectorField, pos);
                    DrawArrow.ForGizmo(pos, force.normalized, Color.grey);
                }
            }
            
        }
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu(fileName = "IslandCheck", menuName = "FSM/StateActions/IslandCheck")]
public class IslandCheck : StateActions
{

    public override void Execute(StateManager states)
    {
        if (Physics2D.Raycast(states.character.transform.position, -(states.character.transform.up), 1f, (LayerMask.GetMask("Island"))))
        {
            states.OnIsland = true;
        }
        else
        {
            states.OnIsland = false;
        }
    }
}

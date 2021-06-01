using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu(fileName = "ClosestSpotCheck", menuName = "FSM/StateActions/ClosestSpotCheck")]
public class ClosestSpotCheck : StateActions
{
    public override void Execute(StateManager states)
    {
        TreeGrow nearestSpot = states.treeSpots[0];

        for (int i = 0; i < states.treeSpots.Count; i++)
        {
            if (Vector2.Distance(states.character.transform.position, nearestSpot.transform.position) >=
             Vector2.Distance(states.character.transform.position, states.treeSpots[i].transform.position))
            {
                nearestSpot = states.treeSpots[i];
            }
        }

        states.nearestSpot = nearestSpot;
    }
}

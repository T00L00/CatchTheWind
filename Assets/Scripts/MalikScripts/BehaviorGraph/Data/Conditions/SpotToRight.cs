using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu(fileName = "SpotToRight", menuName = "FSM/Conditions/SpotToRight")]
public class SpotToRight : Condition
{
    public override bool CheckCondition(StateManager state)
    {
        bool isToTheRight = Vector2.Dot(state.nearestSpot.transform.position - state.character.transform.position, state.character.transform.right) > 0;
        if (isTrue)
        {
            if (isToTheRight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (isToTheRight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

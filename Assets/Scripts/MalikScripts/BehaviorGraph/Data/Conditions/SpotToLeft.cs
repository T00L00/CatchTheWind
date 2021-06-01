using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu(fileName = "SpotToLeft", menuName = "FSM/Conditions/SpotToLeft")]
public class SpotToLeft : Condition
{
    public override bool CheckCondition(StateManager state)
    {
        bool isToTheRight = Vector2.Dot(state.nearestSpot.transform.position - state.character.transform.position, state.character.transform.right) > 0;
        if (isTrue)
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
        else
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
    }
}

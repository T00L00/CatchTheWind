using System.Collections;
using System.Collections.Generic;
using SA;
using UnityEngine;

[CreateAssetMenu(fileName = "OnSpot", menuName = "FSM/Conditions/OnSpot")]
public class OnSpot : Condition
{
    public override bool CheckCondition(StateManager state)
    {
        if (isTrue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

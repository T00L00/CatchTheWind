using System.Collections;
using System.Collections.Generic;
using SA;
using UnityEngine;

[CreateAssetMenu(fileName = "OnIsland", menuName = "FSM/Conditions/OnIsland")]
public class OnIsland : Condition
{
    public override bool CheckCondition(StateManager state)
    {
        if (isTrue)
        {
            if (state.OnIsland)
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
            if (state.OnIsland)
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

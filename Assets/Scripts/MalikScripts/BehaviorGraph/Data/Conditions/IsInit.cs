using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu(fileName = "IsInit", menuName = "FSM/Conditions/IsInit")]
public class IsInit : Condition
{
    public override bool CheckCondition(StateManager state)
    {
        if (isTrue)
        {
            if (state.isInit)
                return true;
            else
                return false;
        }
        else
        {
            if (state.isInit)
                return false;
            else
                return true;
        }
    }
}

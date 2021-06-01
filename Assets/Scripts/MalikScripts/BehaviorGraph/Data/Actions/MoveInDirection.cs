using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;

[CreateAssetMenu(fileName = "MoveInDirection", menuName = "FSM/StateActions/MoveInDirection")]
public class MoveInDirection : StateActions
{
    public bool moveLeft;

    public override void Execute(StateManager states)
    {
        if (moveLeft)
        {
            // states.character
        }
        else
        {

        }
    }
}

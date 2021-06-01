using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;
using System.Linq;

[CreateAssetMenu(fileName = "Init", menuName = "FSM/StateActions/Init")]
public class Init : StateActions
{
    public override void Execute(StateManager states)
    {
        Debug.Log("TEST PLANT SPOTS FOUND");
        states.treeSpots.AddRange(GameObject.FindObjectsOfType<TreeGrow>().ToList());
        states.isInit = true;
    }
}

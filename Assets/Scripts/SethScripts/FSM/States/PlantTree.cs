using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : IState
{
    public void Tick() { }

    public void OnEnter() 
    {
        Debug.Log("State: PlantTree");
    }

    public void OnExit() { }


}

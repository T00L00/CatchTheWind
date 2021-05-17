using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panic : IState
{
    public void Tick() { }

    public void OnEnter() 
    {
        Debug.Log("State: Panic");
    }

    public void OnExit() { }


}

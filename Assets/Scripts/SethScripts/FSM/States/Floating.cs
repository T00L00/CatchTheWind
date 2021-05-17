using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : IState
{
    public void Tick() { }

    public void OnEnter()
    {
        Debug.Log("State: Floating");
    }

    public void OnExit() { }
}
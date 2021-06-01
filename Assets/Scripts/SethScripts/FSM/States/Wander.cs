using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : IState
{
    private readonly Sapling _sapling;

    public Wander(Sapling s)
    {
        _sapling = s;
    }

    public void Tick() 
    {
        
    }

    public void OnEnter() 
    {
        _sapling.animator.SetBool("isGrounded", true);
        Debug.Log("State: Wandering");
    }

    public void OnExit() { }

}

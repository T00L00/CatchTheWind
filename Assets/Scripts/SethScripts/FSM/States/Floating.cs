using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : IState
{
    private readonly Sapling _sapling;

    public Floating(Sapling s)
    {
        _sapling = s;
    }

    public void Tick() { }

    public void OnEnter()
    {
        _sapling.animator.SetBool("isGrounded", false);
        _sapling.groundMovementForce = Vector3.zero;
        _sapling.nearestTreeSpot = null;
        Debug.Log("State: Floating");
    }

    public void OnExit() { }
}
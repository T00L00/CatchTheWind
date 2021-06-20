using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : IState
{
    private readonly Sapling _sapling;

    public PlantTree(Sapling s)
    {
        _sapling = s;
    }

    public void Tick() { }

    public void OnEnter() 
    {
        // Freeze location
        _sapling.rigidBody.velocity = Vector3.zero;
        _sapling.rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        _sapling.animator.SetBool("isGrounded", _sapling.isGrounded);
        _sapling.animator.SetBool("reachedTarget", _sapling.atTreeSpot);
        _sapling.animator.SetBool("hasTarget", _sapling.nearestTreeSpot != null);

        Debug.Log("State: PlantTree");
    }

    public void OnExit() 
    {
        _sapling.animator.SetBool("isGrounded", _sapling.isGrounded);
        _sapling.animator.SetBool("hasTarget", _sapling.nearestTreeSpot == null);
        _sapling.rigidBody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForSiteToPlant : IState
{
    private readonly Sapling _sapling;

    public SearchForSiteToPlant(Sapling sapling)
    {
        _sapling = sapling;
    }

    public void Tick() 
    {
        _sapling.Search();
    }

    public void OnEnter() 
    {
        _sapling.groundMovementForce = _sapling.facing * new Vector3(15, 0);
        _sapling.animator.SetBool("isGrounded", _sapling.isGrounded);
        _sapling.animator.SetFloat("groundMovementForce", Mathf.Abs(_sapling.groundMovementForce.x));
        _sapling.animator.SetBool("enemyNear", _sapling.enemyNear);
        Debug.Log($"State: Searching for site to plant");
    }

    public void OnExit() 
    {
        _sapling.groundMovementForce = Vector3.zero;
    }



}

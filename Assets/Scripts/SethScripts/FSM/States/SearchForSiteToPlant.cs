using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForSiteToPlant : IState
{
    private readonly Sapling _sapling;
    private float speed = 2f;

    public SearchForSiteToPlant(Sapling sapling)
    {
        _sapling = sapling;
    }

    public void Tick() 
    {
        _sapling.FoundTreeSite();
    }

    public void OnEnter() 
    {
        _sapling.groundMovementForce = _sapling.facing * new Vector3(15, 0);
        _sapling.animator.SetBool("isGrounded", true);
        _sapling.animator.SetFloat("groundMovementForce", Mathf.Abs(_sapling.groundMovementForce.x));
        Debug.Log($"State: Searching for site to plant");
    }

    public void OnExit() { }



}

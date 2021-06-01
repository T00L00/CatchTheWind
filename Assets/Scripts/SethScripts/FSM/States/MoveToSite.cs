using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MoveToSite : IState
{
    private readonly Sapling _sapling;
    private float speed = 5.0f;

    public MoveToSite(Sapling sapling)
    {
        _sapling = sapling;
    }

    public void Tick()
    {
        float step = speed * Time.deltaTime;
        _sapling.transform.position = Vector2.MoveTowards(_sapling.transform.position, _sapling.nearestTreeSpot.transform.position, step);
    }

    public void OnEnter() 
    {
        Debug.Log("State: MoveToSite");
        
    }

    public void OnExit() 
    {
        speed = 0f;
    }


}

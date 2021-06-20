using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MoveToSite : IState
{
    private readonly Sapling _sapling;
    private float speed = 2.0f;

    public MoveToSite(Sapling sapling)
    {
        _sapling = sapling;
    }

    public void Tick()
    {
        float step = speed * Time.deltaTime;
        //Vector2 target = new Vector2 { x = _sapling.nearestTreeSpot.transform.position.x, y = _sapling.transform.position.y };
        //_sapling.transform.position = Vector2.MoveTowards(_sapling.transform.position, target, step);

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

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
        
    }

    public void OnEnter() 
    {
        float previousDistance = Mathf.Infinity;
        GameObject nearestTreeSpot = null;
        foreach (GameObject treeSpot in TreeSpotManager.Instance.treeSpots.Keys)
        {
            float distance = Vector2.Distance(_sapling.transform.position, treeSpot.transform.position);
            if ( distance < previousDistance)
            {
                nearestTreeSpot = treeSpot;
                previousDistance = distance;
            }
        }
        _sapling.nearestTreeSpot = nearestTreeSpot;
        Debug.Log($"State: SearchForSiteToPlant | {_sapling.nearestTreeSpot?.name}");
    }

    public void OnExit() { }



}

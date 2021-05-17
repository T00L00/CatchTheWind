using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpotManager : Singleton<TreeSpotManager>
{
    public Dictionary<GameObject, bool> treeSpots;

    private void Awake()
    {
        treeSpots = new Dictionary<GameObject, bool>();
        foreach(GameObject treeSpot in GameObject.FindGameObjectsWithTag("TreeSpot"))
        {
            treeSpots.Add(treeSpot, false);
        }
    } 

    public bool GetTreeSpotStatus(GameObject treeSpot)
    {
        return treeSpots[treeSpot];
    }

}

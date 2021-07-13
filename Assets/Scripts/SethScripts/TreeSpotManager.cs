using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTW
{
    public class TreeSpotManager : MonoBehaviour
    {
        TreeSpot[] treeSpots;

        private void Awake()
        {
            GameObject[] treeSpotsGO = GameObject.FindGameObjectsWithTag("TreeSpot");
            for (int i = 0; i < treeSpotsGO.Length; i++)
            {
                treeSpots[i] = treeSpotsGO[i].GetComponent<TreeSpot>();
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

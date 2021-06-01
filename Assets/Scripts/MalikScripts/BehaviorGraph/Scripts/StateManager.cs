using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class StateManager : MonoBehaviour
    {

        public State currentState;
        public GameObject character;
        public List<TreeGrow> treeSpots = new List<TreeGrow>();
        public TreeGrow nearestSpot;

        public bool isInit;
        public bool OnIsland;
        public bool plantSpotLeft;
        public bool plantSpotRight;
        public bool onPlantSpot;


        [HideInInspector]
        public float delta;
        [HideInInspector]
        public Transform mTransform;

        private void Start()
        {
            mTransform = this.transform;
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Tick(this);
            }
        }
    }
}

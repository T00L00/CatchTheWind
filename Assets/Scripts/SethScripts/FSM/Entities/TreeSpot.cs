using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTW
{
    public class TreeSpot : MonoBehaviour
    {
        public bool planted { get; set; }
        public Animator animator;

        public delegate void OnFinishGrowHandler();
        public event OnFinishGrowHandler OnFinishGrow;

        // Start is called before the first frame update
        void Start()
        {
            planted = false;            
        }

        private void OnEnable()
        {
            OnFinishGrow += UpdatePlantedStatus;
        }

        private void OnDisable()
        {
            OnFinishGrow -= UpdatePlantedStatus;
        }

        public void FinishGrow()
        {
            OnFinishGrow?.Invoke();
        }

        public void UpdatePlantedStatus()
        {
            planted = true;
        }
    }
}

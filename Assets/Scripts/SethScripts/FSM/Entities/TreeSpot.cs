using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        OnFinishGrow += UpdatePlantedStatus;
    }

    public void FinishGrow()
    {
        if (OnFinishGrow != null)
        {
            OnFinishGrow();
        }
    }

    public void UpdatePlantedStatus()
    {
        planted = true;
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == StaticFields.PLAYER_TAG_NAME && planted == false)
    //    {
    //        planted = true;
    //    }
    //}
}

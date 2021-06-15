using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpot : MonoBehaviour
{
    public bool planted { get; set; }
    public Animator animator { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        planted = false;
        animator = GetComponent<Animator>();
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == StaticFields.PLAYER_TAG_NAME && planted == false)
    //    {
    //        planted = true;
    //    }
    //}
}

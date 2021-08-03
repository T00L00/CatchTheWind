using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform sapling;

    void Update()
    {
        if (sapling != null)
        {
            if (sapling.position.y > -4.8 && sapling.position.y < 7f)
                this.transform.position = new Vector3(sapling.position.x,
                           sapling.position.y, this.transform.position.z);
            else
                this.transform.position = new Vector3(sapling.position.x,
                                                     this.transform.position.y, this.transform.position.z);
        }
    }
}

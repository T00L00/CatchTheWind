using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform m_PlayerMovement;

    void Update()
    {
        if(m_PlayerMovement != null)
        {
           if (m_PlayerMovement.position.y > -4.8f && m_PlayerMovement.position.y < 4.7f)
              this.transform.position = new Vector3(m_PlayerMovement.position.x,
                         m_PlayerMovement.position.y, this.transform.position.z);
           else
              this.transform.position = new Vector3(m_PlayerMovement.position.x,
                                                   this.transform.position.y, this.transform.position.z);
        }
    }
}

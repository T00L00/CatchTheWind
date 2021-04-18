using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTree : MonoBehaviour
{
   
    [SerializeField] LevelUp m_LevelUp;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == StaticFields.PLAYER_TAG_NAME)
            m_LevelUp.callLevelUp();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTW
{
    public class FinalTree : MonoBehaviour
    {

        [SerializeField] LevelUp m_LevelUp;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == StaticFields.SAPLING_TAG)
                m_LevelUp.callLevelUp();
        }
    }
}

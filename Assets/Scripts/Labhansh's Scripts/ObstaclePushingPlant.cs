using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTW
{
    public class ObstaclePushingPlant : MonoBehaviour
    {
        [SerializeField] BoxCollider2D m_BoxCollider;
        [SerializeField] Animation m_Animation;

        [SerializeField] AreaEffector2D m_AreaEffector;

        private float m_Force = 1;

        void Start()
        {
            //  m_AreaEffector.forceMagnitude = FORCE;          This is not working. Don't know the reason.
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == StaticFields.SAPLING_TAG)
                StartCoroutine(pushAnim());
        }

        private IEnumerator pushAnim()
        {
            m_Animation.Play();
            yield return new WaitForSeconds(0f);
            m_BoxCollider.usedByEffector = true;

            yield return new WaitForSeconds(5f);
            m_BoxCollider.usedByEffector = false;
            m_Animation.Stop();
        }
    }
}

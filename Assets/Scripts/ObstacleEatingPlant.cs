using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEatingPlant : MonoBehaviour
{
    [SerializeField] Animator m_Animation;
    [SerializeField] LayerMask m_PlayerLM;

    void FixedUpdate()
    {
     
        if (Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0), new Vector2(-1, 1), 1.5f, m_PlayerLM))
        {
            m_Animation.SetTrigger("Eat");
         //   StartCoroutine(eatAnim());
            GameObject.FindGameObjectWithTag(StaticFields.PLAYER_TAG_NAME).GetComponent<PlayerMovement>().Die();
        }
    }

    private IEnumerator eatAnim()
    {
        
        yield return new WaitForSeconds(0.5f);
     
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
    [SerializeField] EdgeCollider2D m_EdgeCollider;
    [SerializeField] Animation m_Animation;

    private bool m_CheckGrowth = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == StaticFields.PLAYER_TAG_NAME && m_CheckGrowth)
        {
            StartCoroutine(growingAnim());
            m_CheckGrowth = false;
        }    
    }

    private IEnumerator growingAnim()
    {
        m_Animation.Play();
        yield return new WaitForSeconds(1f);
        m_Animation.Stop();

        UIManager uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        uiManager.increaseTreeCount();
        uiManager.increaseForestHealth(20f);

    }
}

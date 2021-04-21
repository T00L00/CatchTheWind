using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    [SerializeField] Vector2 m_PrallaxFactor;
    [SerializeField] Transform m_CamPos;
    [SerializeField] SpriteRenderer m_SpriteRenderer;

    private float m_StartPos, m_Length;

    void Start()
    {
        m_StartPos = this.transform.position.x;
        m_Length = m_SpriteRenderer.bounds.size.x;
    }

   
    void Update()
    {
        Vector2 dist = m_CamPos.position * m_PrallaxFactor;


        transform.position = new Vector3(m_StartPos + dist.x, dist.y, transform.position.z);

        if(m_CamPos.position.x > transform.position.x + m_Length)
            m_StartPos += m_Length;        
        else if(m_CamPos.position.x < transform.position.x - m_Length)
            m_StartPos -= m_Length;


    }
}

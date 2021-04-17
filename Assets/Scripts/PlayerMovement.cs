using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_Speed;
    [SerializeField] Rigidbody2D m_Rb;
    [SerializeField] UIManager m_UIManager;

    [SerializeField] GameOver m_GameOver;

    void Update()
    {
     /*   if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
            if(transform.localScale.x < 0)
             transform.localScale = new Vector3( (-1) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * m_Speed * Time.deltaTime);
            if (transform.localScale.x > 0)
                transform.localScale = new Vector3((-1) * transform.localScale.x, transform.localScale.y, transform.localScale.z);

        }

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.up * m_Speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.down * m_Speed * Time.deltaTime); */

    }

    public void Die()
    {
        m_GameOver.callGameOver();
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == StaticFields.SPIKE_TAG_NAME)
        {
            Vector2 reboundForce = m_Rb.velocity * (-1) * 50;
            m_Rb.AddForce(reboundForce);
            m_UIManager.reducePlayerHealth(1);

            if(m_UIManager.getPlayerHealth() <= 0)
                m_GameOver.callGameOver();

        }
    }

   
}

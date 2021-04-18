using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Text References
    [SerializeField] TextMeshProUGUI m_PlayerHealthText;
    [SerializeField] TextMeshProUGUI m_TreeCountText;
    [SerializeField] TextMeshProUGUI m_ForestHealthText;

    //Player Health
    [SerializeField] int m_PlayerStartHealth = 3;
    private int m_PlayerCurrentHealth;
    private const string HEALTH_UI_TEXT = "Health: ";

    // More Forest Health related stuff
    [SerializeField] Slider m_ForestHealthBar;
    [SerializeField] Gradient m_ForestBarGradient;
    [SerializeField] Image m_ForestBarImage;
    [SerializeField] float m_DepletionRate;

    // Tree Count
    [SerializeField] private int m_TotalTreeCount = 5;
    private int m_CurrentTreeCount = 0;
    


    private void Start()
    {
        // Player Health
        m_PlayerHealthText.text = HEALTH_UI_TEXT + m_PlayerStartHealth.ToString();
        m_PlayerCurrentHealth = m_PlayerStartHealth;

        // Tree Count
        m_TreeCountText.text = m_CurrentTreeCount.ToString() + "/" + m_TotalTreeCount.ToString();

        // Forest Health
        m_ForestHealthBar.value = 100;       
        m_ForestBarImage.color = m_ForestBarGradient.Evaluate(1f);
        StartCoroutine(decreaseForestHealth(m_DepletionRate));

    }


    // private

 
    private IEnumerator decreaseForestHealth(float amount)
    {
        while (true)
        {
      //  Debug.Log("Yoo");
            m_ForestHealthBar.value -= amount;
            yield return new WaitForSeconds(0.1f);
            m_ForestBarImage.color = m_ForestBarGradient.Evaluate(m_ForestHealthBar.normalizedValue);

            if (m_ForestHealthBar.value < 30f)
                m_ForestHealthText.enabled = true;
            else
                m_ForestHealthText.enabled = false;

        }
    }



    // public
    public void reducePlayerHealth(int value)
    {
        if (m_PlayerCurrentHealth > 0)
        {
            m_PlayerCurrentHealth -= value;
            m_PlayerHealthText.text = HEALTH_UI_TEXT + m_PlayerCurrentHealth.ToString();
        }
    }
    public int getPlayerHealth()
    {
        return m_PlayerCurrentHealth;
    }

    public void increaseTreeCount()
    {
        m_CurrentTreeCount += 1;
        m_TreeCountText.text = m_CurrentTreeCount.ToString() + "/" + m_TotalTreeCount.ToString();
    }

    public void increaseForestHealth(float amount)
    {
        m_ForestHealthBar.value += amount;
    }



}

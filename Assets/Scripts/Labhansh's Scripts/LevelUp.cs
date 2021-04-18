using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_UiManagerTreeText;
    [SerializeField] TextMeshProUGUI m_FinalTreeScoreText;

    private IEnumerator pauseGame()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    public void callLevelUp()
    {
        this.gameObject.SetActive(true);
        m_FinalTreeScoreText.text = m_UiManagerTreeText.text;
        StartCoroutine(pauseGame());

    }


}

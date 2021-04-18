using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    private int m_CurrentSceneIndex;

    private void Start()
    {
        m_CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void resumeTime()
    {
        Time.timeScale = 1;
    }

    public void restartLevel()
    {
        resumeTime();
        SceneManager.LoadScene(m_CurrentSceneIndex);
    } 

}

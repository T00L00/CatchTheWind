using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void callGameOver()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}

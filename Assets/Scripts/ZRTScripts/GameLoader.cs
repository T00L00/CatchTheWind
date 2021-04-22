using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public void LoadGame(int indexOfLevelToLoad)
    {
        SceneManager.LoadScene(indexOfLevelToLoad);
    }
}

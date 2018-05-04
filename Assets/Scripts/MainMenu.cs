using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public static event Action StartingANewGame;

    public void StartGameButtonClicked()
    {
        // When you load the "game scene" in your menu, just before you call LoadScene
        // do this:
        if (StartingANewGame != null)
        {
            StartingANewGame.Invoke();
        }

        SceneManager.LoadScene("Level Scene");
    }

    public void QuitGameButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

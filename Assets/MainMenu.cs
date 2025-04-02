using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool _isMainMenu = true;

    private GameObject tankNew;
    public void GotToScene(string sceneName)
    {
        if (_isMainMenu)
        {
            Destroy(tankNew);
        }
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Application has been quit!");
    }
}

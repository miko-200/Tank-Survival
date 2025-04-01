using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tank;
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

    /*private void Start()
    {
        tankNew = Instantiate(tank, this.transform);
        tankNew.GetComponent<CharacterCustomization>().enabled = false;
    }*/
}

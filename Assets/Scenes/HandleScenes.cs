using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class HandleScenes : MonoBehaviour
{


    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GoOptions()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial"); // Can technically also be used on retry
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu"); // Loads menu scene
    }




}

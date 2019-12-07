using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <para> Script that used to handle the main menu page of the game. </para>  	
/// </summary>
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        /// <summary>
        /// Command to load the actual game scene.
        /// </summary>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        /// <summary>
        /// Command to exit the application.
        /// </summary>
        Application.Quit();
    }
}

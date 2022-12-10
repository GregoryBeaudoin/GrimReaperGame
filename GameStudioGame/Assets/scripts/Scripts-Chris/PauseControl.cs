using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
	
	public GameObject pauseMenu;
	//public GameObject pauseButton;
    public static bool gameIsPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
	
	public void MainMenuButton()
	{
		PauseGame();
		SceneManager.LoadScene("MainMenu");
	}
	
	public void PauseButton()
	{
		gameIsPaused = !gameIsPaused;
		if(gameIsPaused)
        {
            Time.timeScale = 0f;
			pauseMenu.SetActive(true);
			//pauseButton.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
			pauseMenu.SetActive(false);
			//pauseButton.SetActive(false);
        }
	}
	
    public void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
			pauseMenu.SetActive(true);
			//pauseButton.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
			pauseMenu.SetActive(false);
			//pauseButton.SetActive(false);
        }
    }
	public void exitGame()
	{
		Application.Quit();
	}
}
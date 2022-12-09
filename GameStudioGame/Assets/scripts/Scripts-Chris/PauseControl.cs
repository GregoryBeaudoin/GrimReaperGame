using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
	
	public GameObject pauseMenu;
    public static bool gameIsPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
	
	public void PauseButton()
	{
		gameIsPaused = !gameIsPaused;
		if(gameIsPaused)
        {
            Time.timeScale = 0f;
			pauseMenu.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
			pauseMenu.SetActive(false);
        }
	}
	
    public void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
			pauseMenu.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
			pauseMenu.SetActive(false);
        }
    }
	public void exitGame()
	{
		Application.Quit();
	}
	
	public void options()
	{
		
	}
}
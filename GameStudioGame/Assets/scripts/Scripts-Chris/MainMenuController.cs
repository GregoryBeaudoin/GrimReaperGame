using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
	public GameObject square;
 
 
    public void playGame() {
        SceneManager.LoadScene("Chris");
    }
 
    public void options() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
		square.SetActive(true);
    }
 
    public void back() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
		square.SetActive(false);
    }
 
    public void exitGame() {
        Application.Quit();
    }
}

//Volume: https://www.gamedevelopment.blog/full-unity-2d-game-tutorial-2019-preferences/
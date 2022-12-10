using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;


    public void playGame() {
        SceneManager.LoadScene("Chris");
    }

    public void options() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void back() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void exitGame() {
        Application.Quit();
    }
}

//Volume: https://www.gamedevelopment.blog/full-unity-2d-game-tutorial-2019-preferences/
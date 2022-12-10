using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMainMenu : MonoBehaviour
{
	public void mainMenuSwap() {
		SceneManager.LoadScene("MainMenu");
	}
}
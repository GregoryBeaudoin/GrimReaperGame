using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadPrevious : MonoBehaviour
{
	public void loadPreviousLevel()
	{
		int currentLevel = PlayerPrefs.GetInt("level");
		SceneManager.LoadScene(currentLevel);
	}
}

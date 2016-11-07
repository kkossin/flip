using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class ReturnToMenu : MonoBehaviour
{
	public void StartGame()
	{
		// "Menu" is the scene intended.
		SceneManager.LoadScene("Menu");
	}
}
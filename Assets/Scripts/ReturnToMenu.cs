using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class ReturnToMenu : MonoBehaviour
{
	public void GoToMenu()
	{
        // "Menu" is the scene intended.
        SceneManager.UnloadScene("Scores");
        SceneManager.UnloadScene("Instructions");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
	}
}
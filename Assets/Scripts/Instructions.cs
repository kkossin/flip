using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class Instructions : MonoBehaviour
{
	public void StartGame()
	{
		// "Instructions" is the scene intended.
		SceneManager.LoadScene("Instructions");
	}
}
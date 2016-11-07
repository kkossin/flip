using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class FlipMenu : MonoBehaviour
{
	public void StartGame()
	{
		// "Prototype" is the name of the first scene we created.
		SceneManager.LoadScene("FeatureComplete");
	}
}
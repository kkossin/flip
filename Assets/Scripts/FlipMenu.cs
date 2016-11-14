using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class FlipMenu : MonoBehaviour
{
    private Scene game;
    public int difficulty = 2;

    public void startMusic()
    {
        GetComponent<AudioSource>().Play();
    }

    public void stopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	void startGameEasy()
	{
        // "Prototype" is the name of the first scene we created.       
        difficulty = 1;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("FeatureComplete");  
    }

    void startGameMedium()
    {
        // "Prototype" is the name of the first scene we created.
        difficulty = 2;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("FeatureComplete");
    }

    void startGameHard()
    {
        // "Prototype" is the name of the first scene we created.
        difficulty = 3;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("FeatureComplete");
    }

    void goToInstructions ()
    {
        SceneManager.LoadScene("Instructions");
    }
}
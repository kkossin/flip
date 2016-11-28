using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class FlipMenu : MonoBehaviour
{
    private Scene game;
    public int difficulty;
    private ArrayList scores;

    void Start()
    {
        GetComponent<AudioSource>().Play();
        scores = new ArrayList();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void startMusic()
    {
        GetComponent<AudioSource>().Play();
    }

    public void stopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

	void startGameEasy()
	{      
        difficulty = 1;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("Alpha");  
    }

    void startGameMedium()
    {
        difficulty = 2;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("Alpha");
    }

    void startGameHard()
    {
        difficulty = 3;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("Alpha");
    }

    void goToInstructions ()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void goToScores()
    {
        SceneManager.LoadScene("Scores");
    }

    public void addScore(int score)
    {
        scores.Add(score);
        scores.Sort();
    }

    public int getScore(int position)
    {
        if (scores.Count >= position + 1) return (int)scores[position];
        else return 0;
    }
}
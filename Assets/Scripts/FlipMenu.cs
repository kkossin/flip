using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class FlipMenu : MonoBehaviour
{
    public int difficulty;
    public ArrayList scores;

    void Start()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }       
    }

    void Awake()
    {
        scores = new ArrayList();
        DontDestroyOnLoad(this);     
        GameObject[] instances = GameObject.FindGameObjectsWithTag("Settings");
        if (instances.Length > 1)
        {
            instances[1] = instances[0];
            ArrayList transfer = instances[0].GetComponent<FlipMenu>().scores;
            foreach (int score in transfer)
            {
                addScore(score);
            }
            Destroy(instances[0]);
        }  
    }

    public void startMusic()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
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
        SceneManager.LoadScene("Instructions", LoadSceneMode.Additive);        
    }

    void goToScores()
    {
        SceneManager.LoadScene("Scores", LoadSceneMode.Additive);      
    }

    public void addScore(int score)
    {
        scores.Add(score);
        scores.Sort();
        scores.Reverse();
    }

    public int getScore(int position)
    {
        if (scores.Count >= position + 1) return (int)scores[position];
        else return 0;
    }
}
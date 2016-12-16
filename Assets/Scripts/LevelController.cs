using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
    public float speed;
    private float maxSpeed;
    private int frequency;
    private bool generate;
    private int alternate;
    private Queue<GameObject> activeChunks = new Queue<GameObject>();

    public int score = 0;
    public int difficulty;
    public Text scoreDisplay;
    public Text highScoreDisplay;
    public Text livesDisplay;

    public GameObject levelPlain; //must be a better way to do this...
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject level6;
    public GameObject level7;
    public GameObject level8;
    public GameObject level9;
    public GameObject level10;
    public GameObject level11;
    public GameObject level12;
    public GameObject level13;
    public GameObject level14;
    public GameObject level15;
    public GameObject level16;
    public GameObject level17;
    public GameObject level18;
    public GameObject level19;
    public GameObject life;
    public GameObject shield;
    public GameObject slowTime;

    void Start()
    {
        GetComponent<AudioSource>().Play();
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        highScoreDisplay = GameObject.Find("High Score").GetComponent<Text>();       
        livesDisplay = GameObject.Find("Lives").GetComponent<Text>();
        generate = false;

        if (GameObject.Find("Settings") != null)
        {
            difficulty = GameObject.Find("Settings").GetComponent<FlipMenu>().difficulty;
            highScoreDisplay.text = "High Score: " + GameObject.Find("Settings").GetComponent<FlipMenu>().getScore(0);
        }
        else
        {
            highScoreDisplay.text = "High Score: None";
        }

        switch (difficulty)
        {
            case 0:
                speed = 0.03f;
                maxSpeed = 0.03f;
                frequency = 0;
                break;
            case 1: //Easy
                speed = 0.05f;
                maxSpeed = 0.10f;
                frequency = 1;
                break;
            case 2: //Medium
                speed = 0.06f;
                maxSpeed = 0.12f;
                frequency = 2;
                break;
            case 3: //Hard
                speed = 0.07f;
                maxSpeed = 0.14f;
                frequency = 3;
                break;
        }

        //we start the game with five empty segments
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(-7.5f, 0.0f), Quaternion.identity));
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(-2.5f, 0.0f), Quaternion.identity));
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(2.5f, 0.0f), Quaternion.identity));
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(7.5f, 0.0f), Quaternion.identity));
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(12.5f, 0.0f), Quaternion.identity));      

        int alternate = (int)frequency;
    }

	void FixedUpdate ()
    {
        if (speed < maxSpeed)
        {
            speed = speed * 1.0005f;
        }

	    foreach (GameObject chunk in activeChunks)
        {
            chunk.transform.position = new Vector2(chunk.transform.position.x - speed, 0);
            if (chunk.transform.position.x < -12.5f) { generate = true; } //this bool will maintain the spacing we want
        }

        if (!GameObject.Find("Character").GetComponent<PlayerController>().stopped)
        {
            score += (int)Time.timeScale;
        }
      
        scoreDisplay.text = "Score: " + score.ToString() + "00";
        livesDisplay.text = "Lives: " + GameObject.Find("Character").GetComponent<PlayerController>().lives;

        if (generate)
        {
            GameObject deadChunk = activeChunks.Dequeue();
            Destroy(deadChunk);                 
            int type = 0;
            int spawn = 0;
            if (alternate > 0) { type = Random.Range(1, 20); }  //Generate a random number between 0 and 15 to decide which segment comes next
            spawn = Random.Range(1, 25); //roughly 1 out of 12 sections will generate a powerup
            alternate += 1;
            if (alternate > frequency) { alternate = 0; }

            switch (type) //depending on the number, a level segment is generated
            {             //oddball code style ... please forgive me
                case 0: { activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 1: { activeChunks.Enqueue((GameObject)Instantiate(level1, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 2: { activeChunks.Enqueue((GameObject)Instantiate(level2, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 3: { activeChunks.Enqueue((GameObject)Instantiate(level3, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 4: { activeChunks.Enqueue((GameObject)Instantiate(level4, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 5: { activeChunks.Enqueue((GameObject)Instantiate(level5, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 6: { activeChunks.Enqueue((GameObject)Instantiate(level6, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 7: { activeChunks.Enqueue((GameObject)Instantiate(level7, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 8: { activeChunks.Enqueue((GameObject)Instantiate(level8, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 9: { activeChunks.Enqueue((GameObject)Instantiate(level9, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 10: { activeChunks.Enqueue((GameObject)Instantiate(level10, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 11: { activeChunks.Enqueue((GameObject)Instantiate(level11, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 12: { activeChunks.Enqueue((GameObject)Instantiate(level12, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 13: { activeChunks.Enqueue((GameObject)Instantiate(level13, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 14: { activeChunks.Enqueue((GameObject)Instantiate(level14, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 15: { activeChunks.Enqueue((GameObject)Instantiate(level15, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 16: { activeChunks.Enqueue((GameObject)Instantiate(level16, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 17: { activeChunks.Enqueue((GameObject)Instantiate(level17, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 18: { activeChunks.Enqueue((GameObject)Instantiate(level18, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
                case 19: { activeChunks.Enqueue((GameObject)Instantiate(level19, new Vector2(12.5f, 0.0f), Quaternion.identity));
                          break; }
            }

            switch (spawn)
            {
                case 1:
                    {
                        Instantiate(life, new Vector2(12.5f, 0.0f), Quaternion.identity);
                        break;
                    }
                case 2:
                    {
                        Instantiate(shield, new Vector2(12.5f, 0.0f), Quaternion.identity);
                        break;
                    }
                case 3:
                    {
                        Instantiate(slowTime, new Vector2(12.5f, 0.0f), Quaternion.identity);
                        break;
                    }
            }
            generate = false; //only one segment built per segment that is destroyed
        }
    }

    void setTest()
    {
        difficulty = 0;
    }

    void setEasy()
    {
        difficulty = 1;
    }

    void setMedium()
    {
        difficulty = 2;
    }

    void setHard()
    {
        difficulty = 3;
    }
}

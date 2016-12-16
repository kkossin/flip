using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
    public float speed;
    private float maxSpeed;
    private int selection;
    private bool generate;
    private bool remove;
    private int previous;
    private Queue<GameObject> activeChunks = new Queue<GameObject>();

    public int score = 0;
    public int difficulty;
    private Text scoreDisplay;
    private Text highScoreDisplay;
    private Text livesDisplay;

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
        remove = false;
        previous = 0;

        if (GameObject.Find("Settings") != null)
        {
            difficulty = GameObject.Find("Settings").GetComponent<FlipMenu>().difficulty;
            highScoreDisplay.text = "High Score: " + GameObject.Find("Settings").GetComponent<FlipMenu>().getScore(0) + "00";
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
                selection = 0;
                break;
            case 1: //Easy
                speed = 0.05f;
                maxSpeed = 0.10f;
                selection = 7;
                break;
            case 2: //Medium
                speed = 0.06f;
                maxSpeed = 0.12f;
                selection = 11;
                break;
            case 3: //Hard
                speed = 0.07f;
                maxSpeed = 0.14f;
                selection = 15;
                break;
        }

        //we start the game with five empty segments
        activeChunks.Enqueue(levelPlain);
        levelPlain.transform.position = new Vector2(0f, 0f);    
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
            if (chunk.transform.position.x < -7.48f) { //this will maintain the spacing we want
                if (activeChunks.Count == 1) { generate = true; }
                if (chunk.transform.position.x < -27.5f)
                {
                    remove = true;
                }
            } 
        }

        if (remove)
        {
            GameObject deadChunk = activeChunks.Dequeue();
            deadChunk.transform.position = new Vector2(0, -20);
            remove = false;
        }

        if (!GameObject.Find("Character").GetComponent<PlayerController>().stopped)
        {
            score += (int)Time.timeScale;
        }
      
        scoreDisplay.text = "Score: " + score.ToString() + "00";
        livesDisplay.text = "Lives: " + GameObject.Find("Character").GetComponent<PlayerController>().lives;

        if (generate)
        {
            int type = 0;        
            int spawn = 0;
            type = Random.Range(1, selection);  //Generate a random number based on difficulty
            if (type == previous) {
                if (type == 15) { type = 14; }
                else { type = type + 1; }
            }

            switch (type) //depending on the number, a level segment is generated
            {             //oddball code style ... please forgive me
                case 1: { activeChunks.Enqueue(level1);
                        level1.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 2: { activeChunks.Enqueue(level2);
                        level2.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 3: { activeChunks.Enqueue(level3);
                        level3.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 4: { activeChunks.Enqueue(level4);
                        level4.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 5: { activeChunks.Enqueue(level5);
                        level5.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 6: { activeChunks.Enqueue(level6);
                        level6.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 7: { activeChunks.Enqueue(level7);
                        level7.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 8: { activeChunks.Enqueue(level8);
                        level8.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 9: { activeChunks.Enqueue(level9);
                        level9.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 10: { activeChunks.Enqueue(level10);
                        level10.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 11: { activeChunks.Enqueue(level11);
                        level11.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 12: { activeChunks.Enqueue(level12);
                        level12.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 13: { activeChunks.Enqueue(level13);
                        level13.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 14: { activeChunks.Enqueue(level14);
                        level14.transform.position = new Vector2(27.5f, 0f);
                        break; }
                case 15: { activeChunks.Enqueue(level15);
                        level15.transform.position = new Vector2(27.5f, 0f);
                        break; }                
            }
            previous = type;

            spawn = Random.Range(1, 5); //roughly 1 out of 2 sections will generate a powerup
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

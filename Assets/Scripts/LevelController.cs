using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
    private float speed = 0.05f;
    private bool generate;
    private int alternate;
    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private GameObject[] chunks = new GameObject[]{ };

    private float seconds = 0;
    private int minutes = 0;
    public Text timeDisplay;

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

    void Start()
    {
        generate = false;  //we'll set this as true each time we want a new segment
        alternate = 1; //we alternate between empty and non-empty segments

        //we start the game with five empty segments
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(-7.5f, 0.0f), Quaternion.identity));
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(-2.5f, 0.0f), Quaternion.identity));
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(2.5f, 0.0f), Quaternion.identity));
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(7.5f, 0.0f), Quaternion.identity));
        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(12.5f, 0.0f), Quaternion.identity));

    }
	
	void Update ()
    {
	    foreach (GameObject chunk in activeChunks)
        {
            chunk.transform.position = new Vector2(chunk.transform.position.x - speed, 0);
            if (chunk.transform.position.x < -12.5f) { generate = true; } //this bool will maintain the spacing we want
        }

        if (generate)
        {
            GameObject deadChunk = activeChunks.Dequeue();
            Destroy(deadChunk);
            seconds += Time.timeScale;
            if (seconds >= 60) { seconds = 0; minutes = minutes + 1; }
            //timeDisplay.text = "Score: " + minutes.ToString() + ":" + seconds.ToString();
            timeDisplay.text = "Score: " + seconds.ToString();

            int type = 0;
            if (alternate > 0) { type = Random.Range(0, 16); }  //Generate a random number between 0 and 15 to decide which segment comes next
            alternate += 1;
            if (alternate > 2) { alternate = 0; }

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
            }
            generate = false; //only one segment per segment that is destroyed
        }
    }
}

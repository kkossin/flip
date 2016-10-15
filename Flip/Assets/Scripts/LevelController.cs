using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
    private float speed = 0.05f;
    private bool generate;
    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private GameObject[] chunks = new GameObject[]{ };

    public GameObject levelPlain;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    void Start()
    {
        generate = false;

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
            if (chunk.transform.position.x < -12.5f) { generate = true; }
        }

        if (generate)
        {
            GameObject deadChunk = activeChunks.Dequeue();
            Destroy(deadChunk);
            int type = Random.Range(1, 4);

            switch (type)
            {
                case 0: //not currently used
                    {
                        activeChunks.Enqueue((GameObject)Instantiate(levelPlain, new Vector2(12.5f, 0.0f), Quaternion.identity));
                        break;
                    }
                case 1:
                    {
                        activeChunks.Enqueue((GameObject)Instantiate(level1, new Vector2(12.5f, 0.0f), Quaternion.identity));
                        break;
                    }
                case 2:
                    {
                        activeChunks.Enqueue((GameObject)Instantiate(level2, new Vector2(12.5f, 0.0f), Quaternion.identity));
                        break;
                    }
                case 3:
                    {
                        activeChunks.Enqueue((GameObject)Instantiate(level3, new Vector2(12.5f, 0.0f), Quaternion.identity));
                        break;
                    }
            }
            generate = false;

        }
    }
}

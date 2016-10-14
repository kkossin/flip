using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
    private float speed = 0.05f;
    private bool generate;
    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private GameObject[] chunks = new GameObject[]{ };

    public GameObject levelPlain;

    void Start() {
        generate = false;

        GameObject chunk1 = (GameObject)Instantiate(levelPlain);
        chunk1.transform.position = new Vector2(-12.5f, 0.0f);
        activeChunks.Enqueue(chunk1);

        GameObject chunk2 = (GameObject)Instantiate(levelPlain);
        chunk2.transform.position = new Vector2(-7.5f, 0.0f);
        activeChunks.Enqueue(chunk2);

        GameObject chunk3 = (GameObject)Instantiate(levelPlain);
        chunk3.transform.position = new Vector2(-2.5f, 0.0f);
        activeChunks.Enqueue(chunk3);

        GameObject chunk4 = (GameObject)Instantiate(levelPlain);
        chunk4.transform.position = new Vector2(2.5f, 0.0f);
        activeChunks.Enqueue(chunk4);
            
        GameObject chunk5 = (GameObject)Instantiate(levelPlain);
        chunk5.transform.position = new Vector2(7.5f, 0.0f);
        activeChunks.Enqueue(chunk5);

        GameObject chunk6 = (GameObject)Instantiate(levelPlain);
        chunk6.transform.position = new Vector2(12.5f, 0.0f);
        activeChunks.Enqueue(chunk6);
    }
	
	void Update () {
	    foreach (GameObject chunk in activeChunks)
        {
            chunk.transform.position = new Vector2(chunk.transform.position.x - speed, 0);
            if (chunk.transform.position.x < -12.5f) { generate = true; }
        }

        if (generate)
        {
            GameObject deadChunk = activeChunks.Dequeue();
            Destroy(deadChunk);
            GameObject newChunk = (GameObject)Instantiate(levelPlain);
            newChunk.transform.position = new Vector2(12.5f, 0.0f);
            activeChunks.Enqueue(newChunk);
            generate = false;
        }
    }
}

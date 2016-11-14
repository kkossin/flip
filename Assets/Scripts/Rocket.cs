using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    private static float speed = 0.05f;

    void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    void FixedUpdate () {
        speed = GameObject.Find("Level Manager").GetComponent<LevelController>().speed * 1.25f;

        if (this.transform.position.y > -3)
        {
            this.transform.position = new Vector2(this.transform.position.x - speed, 0);
        }

        if (this.transform.position.x < -12.5)
        {
            Destroy(this);
        }
    }
}

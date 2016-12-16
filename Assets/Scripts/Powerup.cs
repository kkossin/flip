using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

    private float speedHoriz;
    private float speedVerti;

    void Start()
    {
        if (tag != "Rocket")
        {
            speedHoriz = GameObject.Find("Level Manager").GetComponent<LevelController>().speed * 1.25f;
        }
        speedVerti = Random.Range(0.01f, 0.05f); //generate random vertical speed
        int direction = Random.Range(1, 3);      //generate random direction
        if (direction == 2)
        {
            speedVerti = -speedVerti;
        }
    }

    void FixedUpdate()
    {
        if (this.transform.position.y > -3.0f)
        {
            this.transform.position = new Vector2(this.transform.position.x - speedHoriz, this.transform.position.y + speedVerti);
        }

        if (speedVerti < 0.0f && this.transform.position.y < -2.1f)
        {
            speedVerti = -speedVerti;
        }
        if (speedVerti > 0.0f && this.transform.position.y > 2.6f)
        {
            speedVerti = -speedVerti;
        }
        if (this.transform.position.x < -12.5f)
        {
            if (tag != "Rocket") { Destroy(this); }
        }
    }
}

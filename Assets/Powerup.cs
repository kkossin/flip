using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

    private static float speedHoriz = 0.05f;
    private static float speedVerti = 0.02f;

    void FixedUpdate()
    {
        speedHoriz = GameObject.Find("Level Manager").GetComponent<LevelController>().speed * 1.25f;

        if (this.transform.position.y > -3)
        {
            this.transform.position = new Vector2(this.transform.position.x - speedHoriz, this.transform.position.y + speedVerti);
        }

        if (speedVerti < 0 && this.transform.position.y < -2)
        {
            speedVerti = 0.02f;
        }
        if (speedVerti > 0 && this.transform.position.y > 2)
        {
            speedVerti = -0.02f;
        }

        if (this.transform.position.x < -12.5)
        {
            Destroy(this);
        }
    }
}

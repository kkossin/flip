using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private float speedVerti;

	void Start () {
        speedVerti = 0.01f;
	}
	
	void FixedUpdate () {
        if (this.transform.position.y > -3.0f)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + speedVerti);
        }

        if (speedVerti < 0.0f && this.transform.position.y < -0.8f)
        {
            speedVerti = -speedVerti;
        }
        if (speedVerti > 0.0f && this.transform.position.y > 1.3f)
        {
            speedVerti = -speedVerti;
        }
    }
}

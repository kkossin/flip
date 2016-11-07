using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    private static float speed = 0.05f;

	void Update () {
        if (this.transform.position.y > -3)
        {
            this.transform.position = new Vector2(this.transform.position.x - speed, 0);
        }
    }
}

using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {
    private float speed = 0.1f;

	void Start () {
	
	}
	
	void Update () {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}

﻿using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {
    private float speed = 0.05f;
	
	void Update () {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}

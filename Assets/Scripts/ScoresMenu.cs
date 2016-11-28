using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoresMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Slot1").GetComponent<Text>().text = GameObject.Find("Settings").GetComponent<FlipMenu>().getScore(0).ToString() + "00";
        GameObject.Find("Slot2").GetComponent<Text>().text = GameObject.Find("Settings").GetComponent<FlipMenu>().getScore(1).ToString() + "00";
        GameObject.Find("Slot3").GetComponent<Text>().text = GameObject.Find("Settings").GetComponent<FlipMenu>().getScore(2).ToString() + "00";
        GameObject.Find("Slot4").GetComponent<Text>().text = GameObject.Find("Settings").GetComponent<FlipMenu>().getScore(3).ToString() + "00";
        GameObject.Find("Slot5").GetComponent<Text>().text = GameObject.Find("Settings").GetComponent<FlipMenu>().getScore(4).ToString() + "00";
    }
}

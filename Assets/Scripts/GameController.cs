using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private GameObject terrain;
	private GameObject walls;

	// Use this for initialization
	void Start () {
		terrain = GameObject.Find("Terrain");
		walls = GameObject.Find("Walls");
	}
	
	// Update is called once per frame
	void Update () {
		if(Events.on("EndPoint_Near"))
			Debug.Log ("The END!.. is near.");
	}
}

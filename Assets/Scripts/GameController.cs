using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	private GameObject terrain;
	private GameObject walls;
	private GameObject player;
	private FollowerArrow player_arrow;
	private GameObject player_camera;


	public List<GameObject> points = new List<GameObject>();
	private GameObject end;

	private bool playing = true;

	// Use this for initialization
	void Start () {
		terrain = GameObject.Find("Terrain");
		walls = GameObject.Find("Walls");
		player = GameObject.Find("Player");
		player_arrow = player.transform.FindChild("Arrow").GetComponent<FollowerArrow>();
		player_camera = player.transform.FindChild("Camera").gameObject;
		end = GameObject.Find("EndPlatform");

		// Generate Points here(not yet)
		//points.Add(GameObject.Find("Point"));
		
		NextPoint();
	}
	
	// Update is called once per frame
	void Update () {
		if(playing) {
			if(Events.on("EndPlatform_Reached")){
				Debug.Log ("The END!.. or not.");
				playing = false;
				player_arrow.target = null;
			}
			if(Events.on("Point_Reached")) {
				if(NextPoint())
					Debug.Log ("Fine!! Now go to the end platform!");
				else
					Debug.Log ("Fine!! Now go to the next point!");
			}
		}
	}

	private bool NextPoint(){
		if(points.Count <= 0){
			player_arrow.target = end;
			return true;
		}
		player_arrow.target = points[0];
		points.RemoveAt(0);
		return false;
	}
}

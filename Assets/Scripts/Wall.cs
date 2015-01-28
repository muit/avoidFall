using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	private Vector2 pre_position;
	private Vector2 center;

	public GameObject player;

	// Use this for initialization
	void Start () {
		Render ();
	}
	
	// Update is called once per frame
	void Update () {
		pre_position = new Vector2 (
			Mathf.Round (player.transform.position.x), 
			Mathf.Round (player.transform.position.z)
		);
		if(Vector2.Distance(pre_position, center) != 0){
			center = pre_position;
			FixUpdate();
		}
	}

	protected virtual void FixUpdate(){
		Render ();
	}

	protected virtual void Render(){}
}

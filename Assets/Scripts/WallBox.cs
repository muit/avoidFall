using UnityEngine;
using System.Collections;

public class WallBox : Wall {

	// Use this for initialization
	void Start () {
		Render ();
	}
	
	// Update is called once per frame
	protected override void FixUpdate () {
		Render ();
	}

	protected override void Render(){
	}
}

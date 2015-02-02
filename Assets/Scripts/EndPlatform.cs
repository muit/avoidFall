using UnityEngine;
using System.Collections;

public class EndPlatform : MonoBehaviour {
	public GameController gc;
	void Start(){
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider other) {
		if(gc.canFinish && gc.playing)
			Events.call("EndPlatform_Reached");
	}
}

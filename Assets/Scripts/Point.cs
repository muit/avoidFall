using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {
	[System.NonSerialized]
	public GameController gc;
	void Start(){
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}
	void OnTriggerEnter(Collider other) {
		if(other.gameObject == gc.player && gameObject == gc.GetActualPoint()){
			Events.call("Point_Reached");
			Destroy(gameObject);
		}
	}
}

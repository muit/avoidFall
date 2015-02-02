using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {
	public GameController gc;
	void Start(){
		gc = GameObject.Find("GameController").GetComponent<GameController>();
	}
	void OnTriggerEnter(Collider other) {
		if(gameObject == gc.GetActualPoint()){
			Events.call("Point_Reached");
			Destroy(gameObject);
		}
	}
}

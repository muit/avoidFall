using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		Events.call("Point_Reached");
		Destroy(gameObject);
	}
}

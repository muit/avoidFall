using UnityEngine;
using System.Collections;

public class EndPlatform : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		Events.call("EndPlatform_Reached");
	}
}

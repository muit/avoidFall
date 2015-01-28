using UnityEngine;
using System.Collections;

public class FollowerArrow : MonoBehaviour {
	public GameObject player;
	public GameObject target;
	public float disappearDistance = 7;

	private bool t_disabled;
	private MeshRenderer mesh;
	// Use this for initialization
	void Start () {
		mesh = GetComponentInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.transform.position, target.transform.position) > disappearDistance) {
			if (!mesh.enabled)
				mesh.enabled = !mesh.enabled;
			transform.LookAt (target.transform);
		} else if (mesh.enabled){
			mesh.enabled = !mesh.enabled;
			Events.call("EndPoint_Near");
		}
	}
}

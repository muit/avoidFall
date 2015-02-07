using UnityEngine;
using System.Collections;

public class MapDespawner : MonoBehaviour {
	private GameObject terrain;
	private Generator generator;
	// Use this for initialization
	void Start () {
		generator = (terrain = GameObject.Find("Terrain")).GetComponent<Generator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other) {
		if(other.transform.IsChildOf(terrain.transform) && other.name.Contains(":")){
			string[] xy = other.name.Split(':');
			generator.RemoveCube(new Vector2(float.Parse(xy[0]), float.Parse(xy[1])));
        }
    }
}

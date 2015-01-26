using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	private SimplexNoise noise = null;

	public int render_range = 5;
	public string seed;

	public GameObject player;

	//Noise Attributes
	public int octaves = 1;
	public int multiplier = 25;
	public float amplitude = 0.5f;
	public float lacunarity = 2;
	public float persistence = 0.9f;

	void Start () {
		noise = new SimplexNoise (seed);
		noise.setup (octaves, multiplier, amplitude, lacunarity, persistence);
	}

	void Update () {
		//noise.getCoherent (0, 0, 0);
	}
}

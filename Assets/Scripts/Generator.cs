using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	private SimplexNoise noise = null;

	public int rangeHeight = 10;
	public int rangeWidth = 5;
	public string seed = "sweetcandy";
	
	private Vector2 pre_position;
	public Vector2 center;
	public GameObject player;
	private RectangleRange range;

	//Noise Attributes
	public int   octaves     = 1;
	public float multiplier  = 0.1f;
	public float amplitude   = 1;
	public float lacunarity  = 1;
	public float persistence = 1;
	public float high        = 2;
	public float low         = -2;

	void Start () {
		noise = new SimplexNoise (seed);
		noise.setup (octaves, multiplier, amplitude, high, low, lacunarity, persistence);
		range = new RectangleRange (this, rangeHeight, rangeWidth);
	}

	void Update () {
		pre_position = new Vector2 (
			Mathf.Round (player.transform.position.x), 
			Mathf.Round (player.transform.position.z)
		);
		if(Vector2.Distance(pre_position, center) != 0){
			center = pre_position;
			range.Update(center);
		}
		//noise.getCoherent (0, 0, 0);
	}

	void NewCube(Vector2 position){
		//if(cubes[position.x][position.y] != null)
			float y = noise.getCoherent (position.x, 0, position.y);
			Debug.Log (new Vector3 (position.x, y, position.y));
	}

	public class Range {
		private Generator generator;

		public Range(Generator generator){
			this.generator = generator;
		}

		public virtual void Update(Vector2 center){}
	}

	public class RectangleRange : Range{
		public int height;
		public int width;

		public RectangleRange(Generator generator, int height, int width) : base(generator){
			this.height = height;
			this.width = width;
		}

		public override void Update(Vector2 center){
			Debug.Log ("Rendering new Blocks...");



		}
	}
}

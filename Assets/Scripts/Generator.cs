using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {
	private SimplexNoise noise = null;
	private RectangleRange range;
	private Vector2 pre_position;
	private Vector2 center;
	private TerrainCache tc = new TerrainCache ();

	public int rangeHeight = 10;
	public int rangeWidth = 5;
	public string seed = "sweetcandy";
	public GameObject player;
	public GameObject cubePrefab;
	//Noise Attributes
	public int   octaves     = 1;
	public float frequency  = 0.1f;
	public float amplitude   = 1;
	public float lacunarity  = 1;
	public float persistence = 1;
	public float high        = 2;
	public float low         = -2;

	void Start () {
		noise = new SimplexNoise (seed);
		noise.setup (octaves, frequency, amplitude, high, low, lacunarity, persistence);
		range = new RectangleRange (this);

		center = new Vector2 (
			Mathf.Round (player.transform.position.x), 
			Mathf.Round (player.transform.position.z)
		);
		range.Update(center);
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
		if (tc.Get (position) == null) {
			float y = noise.getCoherent (position.x, 0, position.y);
			GameObject go = Instantiate(cubePrefab, new Vector3 (position.x, y, position.y), Quaternion.identity) as GameObject;
			go.transform.parent = transform;
			go.SetActive(true);
			tc.Set(position, go);
		}
	}

	public class Range {
		protected Generator generator;

		public Range(Generator generator){
			this.generator = generator;
		}

		public virtual void Update(Vector2 center){}
	}

	public class RectangleRange : Range{
		public RectangleRange(Generator generator) : base(generator){
		}

		public override void Update(Vector2 center){
			for (int x = (int)center.x-generator.rangeWidth; x<=(int)center.x+generator.rangeWidth; x++){
				for (int y = (int)center.y-generator.rangeHeight; y<=(int)center.y+generator.rangeHeight; y++){
					generator.NewCube(new Vector2(x,y));
				}
			}
		}
	}

	public class TerrainCache: Dictionary<string, GameObject>  {
		public GameObject Get(Vector2 pos) {
			try{
				return this [pos.x + ":" + pos.y];
			}catch(KeyNotFoundException){
				return null;
			}
		}
		public void Set(Vector2 pos, GameObject go) {
			go.name = pos.x + ":" + pos.y;
			this.Add (go.name, go);
		}
		public void Replace(Vector2 pos, GameObject go) {
			this [pos.x + ":" + pos.y] = go;
		}
	}
}

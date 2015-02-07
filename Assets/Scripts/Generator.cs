using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {
	private static int NULL_FLOAT = -111111;
	private SimplexNoise noise = null;
	private RectangleRange spawnRange;
	private BoxCollider despawnRange;
	private Vector2 pre_position;
	private Vector2 center;
	private TerrainCache tc = new TerrainCache ();
	private TerrainDataStorage tcs = new TerrainDataStorage ();

	public int spawnRangeHeight = 8;
	public int spawnRangeWidth = 8;
	public int despawnRangeHeight = 10;
	public int despawnRangeWidth = 10;
	public string seed = "sweetcandy";
	public GameObject player;
	private SphereCollider mapDespawner;
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
		spawnRange = new RectangleRange (this);
		despawnRange = GetComponent<BoxCollider>();

		center = new Vector2 (
			Mathf.Round (player.transform.position.x), 
			Mathf.Round (player.transform.position.z)
		);
		
		mapDespawner = player.GetComponentInChildren<SphereCollider>();
		spawnRange.Update(center);
	}

	void Update () {
		pre_position = new Vector2 (
			Mathf.Round (player.transform.position.x), 
			Mathf.Round (player.transform.position.z)
		);
		if(Vector2.Distance(pre_position, center) != 0){
			center = pre_position;
			spawnRange.Update(center);
		}
	}

	float NewCube(Vector2 position){
		if (tc.Get (position) == null) {
			float y = tcs.Get (position);
			if(y == NULL_FLOAT){
				Debug.Log ("sss");
				y = noise.getCoherent (position.x, 0, position.y);
				tcs.Set (position, y);
			}

			GameObject go = Instantiate(cubePrefab, new Vector3 (position.x, y, position.y), Quaternion.identity) as GameObject;
			go.transform.parent = transform;
			go.SetActive(true);
			tc.Set(position, go);
			return y;
		}
		return NULL_FLOAT;
	}

	float NewCube(Vector3 position){
		Vector2 position_2d = new Vector2 (position.x, position.z);
		if (tc.Get (position_2d) == null) {
			tcs.Replace (position, position.y);
			GameObject go = Instantiate(cubePrefab, position, Quaternion.identity) as GameObject;
			go.transform.parent = transform;
			go.SetActive(true);
			tc.Set(position_2d, go);
			return position.y;
		}
		return NULL_FLOAT;
	}

	public void RemoveCube(Vector2 position){
		if (tc.Get (position) != null)
			tc.Remove(position);
	}

	public class Range {
		protected Generator generator;

		public Range(Generator generator){
			this.generator = generator;
		}

		public virtual void Update(Vector2 center){}
	}

	public class RectangleRange : Range{
		private bool inverted;
		public RectangleRange(Generator generator) : base(generator){}

		public override void Update(Vector2 center){
			for (float x = (int)center.x-generator.spawnRangeWidth; x<=(int)center.x+generator.spawnRangeWidth; x++){
				for (float z = (int)center.y-generator.spawnRangeHeight; z<=(int)center.y+generator.spawnRangeHeight; z++){
					generator.NewCube(new Vector2(x,z));
				}
			}
		}
	}




	public class TerrainCache : Dictionary<Vector2, GameObject>  {
		//Need to Implement Chunks and distance clearing
		public GameObject Get(Vector2 pos) {
			try{
				return this [pos];
			}catch(KeyNotFoundException){
				return null;
			}
		}
		public void Set(Vector2 pos, GameObject go) {
			go.name = pos.x + ":" + pos.y;
			this.Add (pos, go);
		}
		public void Replace(Vector2 pos, GameObject go) {
			this [pos] = go;
		}

		public void Remove(Vector2 pos){
			Animation animation = base[pos].GetComponent<Animation>();
			animation.Play("Block_Despawn");
			GameObject.Destroy(base[pos], 0.5f);

			base.Remove(pos);
		}
	}


	public class TerrainDataStorage : Dictionary<Vector2, float> {
		public float Get(Vector2 pos) {
			try{
				return this [pos];
			}catch(KeyNotFoundException){
				return NULL_FLOAT;
			}
		}

		public void Set(Vector3 position) {
			this.Add (new Vector2(position.x, position.z), position.y);
		}
		public void Set(Vector2 position, float y) {
			this.Add (position, y);
		}

		public void Replace(Vector2 pos, float y) {
			try{
				this [pos] = y;
			}catch(KeyNotFoundException){
				this.Add(pos, y);
			}
		}
	}
}

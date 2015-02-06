using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
	public float fowardSpeed = 1;
	public float backwardSpeed = 1;
	public float sidewardSpeed = 1;

	
	[System.NonSerialized]
	public Vector3 inputMoveDirection;
	[System.NonSerialized]
	public bool inputJump;	
	[System.NonSerialized]
	public CharacterController controller;	

	void Start () {
		controller = GetComponent<CharacterController>();
	}

	void Update () {
		controller.Move(Vector3.zero);
	}
}

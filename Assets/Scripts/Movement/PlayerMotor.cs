using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
	public float fowardSpeed = 1;
	public float backwardSpeed = 1;
	public float sidewardSpeed = 1;

	
	[System.NonSerialized]
	public Vector3 inputMoveDirection = Vector3.zero;
	[System.NonSerialized]
	public bool inputJump = false;	
	[System.NonSerialized]
	private CharacterController controller;	
	[System.NonSerialized]
	private Animator animator;	

	void Start () {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	void Update () {
		Debug.Log(inputMoveDirection);
		animator.SetFloat("frontSpeed", inputMoveDirection.z); 
		animator.SetFloat("sideSpeed", inputMoveDirection.x); 
		controller.Move(Vector3.zero);
	}
}

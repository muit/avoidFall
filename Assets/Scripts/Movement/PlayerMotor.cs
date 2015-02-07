using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
	public float fowardSpeed = 1;
	public float backwardSpeed = 1;
	public float sidewardSpeed = 1;

	public Generator generator;

	
	[System.NonSerialized]
	public Vector3 inputMoveDirection = Vector3.zero;
	[System.NonSerialized]
	public bool inputJump = false;

	private CharacterController controller;
	private Animator animator;	
	private bool jumping = false;

	void Start () {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	void Update () {
		animator.SetFloat("frontSpeed", inputMoveDirection.z); 
		animator.SetFloat("sideSpeed", inputMoveDirection.x);
		animator.SetBool ("jump",inputJump);
		controller.Move(Vector3.zero);
	}
}

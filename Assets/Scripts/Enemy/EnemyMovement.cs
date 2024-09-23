using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

	public float startVelocity;
	public float rotationSpeed;

	private ShipInputController inputController;
	private new Rigidbody rigidbody;

	// Use this for initialization
	void Start()
	{
		inputController = GetComponent<ShipInputController>();
		rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateMoveAdaptiveRotation();
	}

	private void UpdateMoveAdaptiveRotation()
	{
		var inputDirection = new Vector3(inputController.horizontal, 0, inputController.vertical);
		var thrust = Vector3.Dot(inputDirection.normalized, this.transform.forward);
		var rotation = Vector3.Dot(inputDirection.normalized, this.transform.right);

		this.rigidbody.AddForce(thrust * inputDirection.magnitude *
				this.transform.forward * startVelocity * Time.deltaTime);
		var rotationAmount = rotationSpeed * Time.deltaTime * rotation;
		this.rigidbody.AddTorque(0, rotationAmount, 0);
	}
}
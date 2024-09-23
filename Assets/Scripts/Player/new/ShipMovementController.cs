using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipMovementType
{
	Slide,
	ManualRotation,
	AdaptiveRotation
}

public class ShipMovementController : MonoBehaviour
{

	public float velocity;
	public float rotationSpeed;
	//public ShipMovementType movementType;

	private ShipInputController inputController;

	void Start()
	{

		inputController = GetComponent<ShipInputController>();
	}

	void Update()
	{
		/*switch (movementType)
		{
			case ShipMovementType.ManualRotation:
				UpdateMoveManualRotation();
				return;
			case ShipMovementType.AdaptiveRotation:
				if(isStopped == false) UpdateMoveAdaptiveRotation();
				return;
			case ShipMovementType.Slide:
			default:
				UpdateMoveSlide();
				return;
		}*/

		UpdateMoveAdaptiveRotation();
	}

	/*private void UpdateMoveManualRotation()
	{
		this.transform.position += inputController.vertical * this.transform.forward * velocity * Time.deltaTime;
		var rotationAmount = rotationSpeed * Time.deltaTime * inputController.horizontal;
		this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y + rotationAmount, 0);
	}*/

	private void UpdateMoveAdaptiveRotation()
	{
		var inputDirection = new Vector3(inputController.horizontal, 0, /*inputController.vertical*/0);
		var thrust = Vector3.Dot(inputDirection.normalized, this.transform.forward);
		var rotation = Vector3.Dot(inputDirection.normalized, this.transform.right);
		this.transform.position += thrust * inputDirection.magnitude *
						this.transform.forward * velocity * Time.deltaTime;
		var rotationAmount = rotationSpeed * Time.deltaTime * rotation;
		this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y + rotationAmount, 0);
	}

	/*private void UpdateMoveSlide()
	{
		this.transform.position += inputController.horizontal * Vector3.right * velocity * Time.deltaTime;
		this.transform.position += inputController.vertical * Vector3.forward * velocity * Time.deltaTime;
	}*/


	private void OnTriggerEnter(Collider other)
    {
		
	}
}
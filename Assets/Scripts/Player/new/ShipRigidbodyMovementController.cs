using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRigidbodyMovementController : MonoBehaviour
{

	public float startVelocity;
	public float rotationSpeed;
	public float currentVelosity;

	[Header("BoostSettings")]
	[SerializeField] private float boostVelocityValue;
	[SerializeField] private float timeBoostValue = 2f;
	//[SerializeField] private float dangerousVelocity;

	private ShipInputController inputController;
	private new Rigidbody rigidbody;

	// Use this for initialization
	void Start()
	{
		currentVelosity = startVelocity;

		inputController = GetComponent<ShipInputController>();
		rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateMoveAdaptiveRotation();

        if (Input.GetKey(KeyCode.LeftShift))
        {
			currentVelosity = Mathf.Lerp(currentVelosity, boostVelocityValue, timeBoostValue * Time.deltaTime);
		}
        else
        {
			currentVelosity = Mathf.Lerp(currentVelosity, startVelocity, timeBoostValue * Time.deltaTime);
		}
	}

	private void UpdateMoveAdaptiveRotation()
	{
		var inputDirection = new Vector3(inputController.horizontal, 0, inputController.vertical);
		var thrust = Vector3.Dot(inputDirection.normalized, this.transform.forward);
		var rotation = Vector3.Dot(inputDirection.normalized, this.transform.right);

		this.rigidbody.AddForce(thrust * inputDirection.magnitude *
				this.transform.forward * currentVelosity * Time.deltaTime);
		var rotationAmount = rotationSpeed * Time.deltaTime * rotation;
		this.rigidbody.AddTorque(0, rotationAmount, 0);
	}

	/*void OnCollisionEnter(Collision collision)
	{
		// Получить объект, с которым произошло столкновение
		GameObject otherObject = collision.gameObject;

		// Проверить тег объекта
		if (otherObject != null && (currentVelosity >= dangerousVelocity))
		{
			Debug.Log("НЕ ТОПИ, ПРИВЫКНИ К АППАРАТУ");
		}
	}*/
}
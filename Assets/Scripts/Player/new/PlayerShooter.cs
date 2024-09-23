using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour
{
	public Transform firePoint;
	public GameObject projectilePrefab;

	public int damage = 100;

    void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject go = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
			go.GetComponent<LaserProjectile>().Init(damage);
			Destroy(go, 1f);
		}
	}
}

using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
	[SerializeField] private float projectileSpeed;
    private int damage;

    public void Init(int damage)
    {
        this.damage = damage;

        // ���������� �������� ���� �� ��� X � 0 ��������
        Vector3 initialRotation = transform.rotation.eulerAngles;
        initialRotation.x = 0f;
        transform.rotation = Quaternion.Euler(initialRotation);
    }

    void Update()
	{
		transform.position += transform.forward * Time.deltaTime * projectileSpeed;
	}

    void OnCollisionEnter(Collision collision)
    {
        // �������� ������, � ������� ��������� ������������
        GameObject otherObject = collision.gameObject;

        // ��������� ��� �������
        if (otherObject.CompareTag("Enemy"))
        {
            otherObject.GetComponent<EnemyHealth>().HealthReduce(damage);
        }
        else if (otherObject.CompareTag("Player"))
        {
            Debug.Log("����");
        }

        Destroy(gameObject);
    }
}

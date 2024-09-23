using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
	[SerializeField] private float projectileSpeed;
    private int damage;

    public void Init(int damage)
    {
        this.damage = damage;

        // Установить вращение куба по оси X в 0 градусов
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
        // Получить объект, с которым произошло столкновение
        GameObject otherObject = collision.gameObject;

        // Проверить тег объекта
        if (otherObject.CompareTag("Enemy"))
        {
            otherObject.GetComponent<EnemyHealth>().HealthReduce(damage);
        }
        else if (otherObject.CompareTag("Player"))
        {
            Debug.Log("Саси");
        }

        Destroy(gameObject);
    }
}

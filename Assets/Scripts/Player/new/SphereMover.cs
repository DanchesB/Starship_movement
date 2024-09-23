using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour
{
    public Transform target; // Объект, к которому привязана сфера
    public float maxDistance = 5.0f; // Максимальное расстояние от целевого объекта, на которое сфера может удалиться
    public float moveSpeed = 5.0f; // Скорость перемещения сферы

    void Update()
    {
        // Получаем ввод от клавиш WASD
        float moveX = 0;
        float moveZ = 0;

        if (Input.GetKey(KeyCode.W))
        {
            moveZ = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }

        // Вычисляем новое направление перемещения
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // Обновляем позицию сферы
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Проверяем, насколько новое положение сферы близко к привязанному объекту
        float distance = Vector3.Distance(newPosition, target.position);

        // Если сфера остается в пределах максимального расстояния, обновляем ее позицию
        if (distance <= maxDistance)
        {
            transform.position = newPosition;
        }
    }
}

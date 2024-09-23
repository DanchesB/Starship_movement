using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour
{
    public Transform target; // ������, � �������� ��������� �����
    public float maxDistance = 5.0f; // ������������ ���������� �� �������� �������, �� ������� ����� ����� ���������
    public float moveSpeed = 5.0f; // �������� ����������� �����

    void Update()
    {
        // �������� ���� �� ������ WASD
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

        // ��������� ����� ����������� �����������
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // ��������� ������� �����
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // ���������, ��������� ����� ��������� ����� ������ � ������������ �������
        float distance = Vector3.Distance(newPosition, target.position);

        // ���� ����� �������� � �������� ������������� ����������, ��������� �� �������
        if (distance <= maxDistance)
        {
            transform.position = newPosition;
        }
    }
}

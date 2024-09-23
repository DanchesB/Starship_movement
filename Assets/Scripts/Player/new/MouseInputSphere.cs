using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputSphere : MonoBehaviour
{
    public Camera mainCamera; // �������� ������

    public LayerMask myLayerMask;
    
    void Start()
    {
        // ���� �������� ������ �� ������, �������� �������� ������ �� ���������
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // �������� ������� ������� ���� � �������� �����������
        Vector3 mousePosition = Input.mousePosition;

        // ��������� Raycast �� ������� ������� ����
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // ���� ��� ���������� ������ � �����
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            // �������� ����� �����������
            Vector3 hitPoint = hit.point;

            // ���������� Y-���������� � �������� �������� (��������, 0) ��� ��������� XZ
            hitPoint.y = 0;

            // ����������� ����� � ����� ����������� �� ���� X � Z
            transform.position = hitPoint;
        }
    }
}

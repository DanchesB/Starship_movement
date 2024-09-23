using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputSphere : MonoBehaviour
{
    public Camera mainCamera; // Основная камера

    public LayerMask myLayerMask;
    
    void Start()
    {
        // Если основная камера не задана, получить основную камеру по умолчанию
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Получить позицию курсора мыши в экранных координатах
        Vector3 mousePosition = Input.mousePosition;

        // Выполнить Raycast из позиции курсора мыши
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // Если луч пересекает объект в сцене
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            // Получить точку пересечения
            Vector3 hitPoint = hit.point;

            // Установить Y-координату в желаемое значение (например, 0) для плоскости XZ
            hitPoint.y = 0;

            // Переместить сферу в точку пересечения по осям X и Z
            transform.position = hitPoint;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    // Объект, который нужно сгенерировать
    public GameObject prefab;

    // Частота появления объекта (количество раз)
    public int spawnCount;

    // Список сгенерированных объектов для отслеживания
    public List<GameObject> spawnedObjects = new List<GameObject>();
}

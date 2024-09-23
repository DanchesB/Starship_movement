using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelGenerator : MonoBehaviour
{
    // Плоскость, которая будет создаваться на уровне
    //public GameObject planePrefab;

    public GameObject Plane;

    // Список объектов для генерации
    public List<SpawnableObject> spawnableObjects;

    // Размер плоскости
    private const float PlaneSize = 100f;

    // Начальная позиция плоскости
    private readonly Vector3 planeStartPosition = new Vector3(0, 0, 0);

    // Общее минимальное расстояние между одним видом объектов
    public float minDistanceSameType = 1.0f; // Установите значение по умолчанию или настройте его в инспекторе

    // Общее минимальное расстояние между разными видами объектов
    public float minDistanceDifferentTypes = 2.0f; // Установите значение по умолчанию или настройте его в инспекторе

    // Ссылка на пустой объект "Environment" в иерархии, где будут спавниться все объекты
    public Transform environmentParent;

    void Start()
    {
        // Создание плоскости
        //GameObject plane = Instantiate(planePrefab, planeStartPosition, Quaternion.identity);

        // Установите родителя для плоскости как environmentParent
        //plane.transform.SetParent(environmentParent);

        // Генерация объектов на плоскости
        GenerateObjects();
        GetComponent<NavMeshSurface>().BuildNavMesh();
        Plane.GetComponent<MeshRenderer>().enabled = false;
    }

    void GenerateObjects()
    {
        // Список для отслеживания сгенерированных объектов
        List<GameObject> allSpawnedObjects = new List<GameObject>();

        // Подсчет количества сгенерированных объектов для каждого типа
        int[] spawnCounts = new int[spawnableObjects.Count];

        // Пока не будут сгенерированы все объекты
        while (true)
        {
            bool allDone = true;

            // Проход по каждому типу объектов
            for (int i = 0; i < spawnableObjects.Count; i++)
            {
                var spawnable = spawnableObjects[i];

                // Если текущий тип объектов еще не сгенерировал все объекты
                if (spawnCounts[i] < spawnable.spawnCount)
                {
                    allDone = false;

                    Vector3 position;
                    bool positionIsValid;

                    do
                    {
                        // Генерация случайных координат на плоскости
                        float x = Random.Range(-PlaneSize * 5, PlaneSize * 5);
                        float z = Random.Range(-PlaneSize * 5, PlaneSize * 5);
                        position = new Vector3(x, 0, z);

                        // Проверка, чтобы объекты не спавнились друг в друге
                        positionIsValid = true;

                        // Проверка расстояния между одним видом объектов
                        foreach (var existingObject in spawnable.spawnedObjects)
                        {
                            if (Vector3.Distance(position, existingObject.transform.position) < minDistanceSameType)
                            {
                                positionIsValid = false;
                                break;
                            }
                        }

                        // Проверка расстояния между разными видами объектов
                        foreach (var existingObject in allSpawnedObjects)
                        {
                            if (Vector3.Distance(position, existingObject.transform.position) < minDistanceDifferentTypes)
                            {
                                positionIsValid = false;
                                break;
                            }
                        }

                    } while (!positionIsValid);

                    // Создание объекта и добавление его в список сгенерированных объектов
                    GameObject spawnedObject = Instantiate(spawnable.prefab, position, Quaternion.identity);

                    // Установите родителя объекта как environmentParent
                    spawnedObject.transform.SetParent(environmentParent);

                    spawnable.spawnedObjects.Add(spawnedObject);
                    allSpawnedObjects.Add(spawnedObject);

                    // Увеличение счетчика сгенерированных объектов
                    spawnCounts[i]++;
                }
            }

            // Если все объекты были сгенерированы, выходим из цикла
            if (allDone)
            {
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelGenerator : MonoBehaviour
{
    // ���������, ������� ����� ����������� �� ������
    //public GameObject planePrefab;

    public GameObject Plane;

    // ������ �������� ��� ���������
    public List<SpawnableObject> spawnableObjects;

    // ������ ���������
    private const float PlaneSize = 100f;

    // ��������� ������� ���������
    private readonly Vector3 planeStartPosition = new Vector3(0, 0, 0);

    // ����� ����������� ���������� ����� ����� ����� ��������
    public float minDistanceSameType = 1.0f; // ���������� �������� �� ��������� ��� ��������� ��� � ����������

    // ����� ����������� ���������� ����� ������� ������ ��������
    public float minDistanceDifferentTypes = 2.0f; // ���������� �������� �� ��������� ��� ��������� ��� � ����������

    // ������ �� ������ ������ "Environment" � ��������, ��� ����� ���������� ��� �������
    public Transform environmentParent;

    void Start()
    {
        // �������� ���������
        //GameObject plane = Instantiate(planePrefab, planeStartPosition, Quaternion.identity);

        // ���������� �������� ��� ��������� ��� environmentParent
        //plane.transform.SetParent(environmentParent);

        // ��������� �������� �� ���������
        GenerateObjects();
        GetComponent<NavMeshSurface>().BuildNavMesh();
        Plane.GetComponent<MeshRenderer>().enabled = false;
    }

    void GenerateObjects()
    {
        // ������ ��� ������������ ��������������� ��������
        List<GameObject> allSpawnedObjects = new List<GameObject>();

        // ������� ���������� ��������������� �������� ��� ������� ����
        int[] spawnCounts = new int[spawnableObjects.Count];

        // ���� �� ����� ������������� ��� �������
        while (true)
        {
            bool allDone = true;

            // ������ �� ������� ���� ��������
            for (int i = 0; i < spawnableObjects.Count; i++)
            {
                var spawnable = spawnableObjects[i];

                // ���� ������� ��� �������� ��� �� ������������ ��� �������
                if (spawnCounts[i] < spawnable.spawnCount)
                {
                    allDone = false;

                    Vector3 position;
                    bool positionIsValid;

                    do
                    {
                        // ��������� ��������� ��������� �� ���������
                        float x = Random.Range(-PlaneSize * 5, PlaneSize * 5);
                        float z = Random.Range(-PlaneSize * 5, PlaneSize * 5);
                        position = new Vector3(x, 0, z);

                        // ��������, ����� ������� �� ���������� ���� � �����
                        positionIsValid = true;

                        // �������� ���������� ����� ����� ����� ��������
                        foreach (var existingObject in spawnable.spawnedObjects)
                        {
                            if (Vector3.Distance(position, existingObject.transform.position) < minDistanceSameType)
                            {
                                positionIsValid = false;
                                break;
                            }
                        }

                        // �������� ���������� ����� ������� ������ ��������
                        foreach (var existingObject in allSpawnedObjects)
                        {
                            if (Vector3.Distance(position, existingObject.transform.position) < minDistanceDifferentTypes)
                            {
                                positionIsValid = false;
                                break;
                            }
                        }

                    } while (!positionIsValid);

                    // �������� ������� � ���������� ��� � ������ ��������������� ��������
                    GameObject spawnedObject = Instantiate(spawnable.prefab, position, Quaternion.identity);

                    // ���������� �������� ������� ��� environmentParent
                    spawnedObject.transform.SetParent(environmentParent);

                    spawnable.spawnedObjects.Add(spawnedObject);
                    allSpawnedObjects.Add(spawnedObject);

                    // ���������� �������� ��������������� ��������
                    spawnCounts[i]++;
                }
            }

            // ���� ��� ������� ���� �������������, ������� �� �����
            if (allDone)
            {
                break;
            }
        }
    }
}

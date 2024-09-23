using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    // ������, ������� ����� �������������
    public GameObject prefab;

    // ������� ��������� ������� (���������� ���)
    public int spawnCount;

    // ������ ��������������� �������� ��� ������������
    public List<GameObject> spawnedObjects = new List<GameObject>();
}

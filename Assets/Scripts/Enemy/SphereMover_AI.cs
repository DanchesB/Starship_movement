using UnityEngine;
using UnityEngine.AI;

public class SphereMover_AI : MonoBehaviour
{
    public Transform Target; // ������� ������, � �������� ����� ������ ���������
    public Transform parentObject; // ������������ ������
    public float distanceFromParent = 2.0f; // ���������� �� ������������� �������
    
    private NavMeshAgent agent;

    public void Initialize(Transform target)
    {
        this.Target = target;
    }

    void Start()
    {
        // �������� NavMeshAgent ���������� �����
        agent = GetComponent<NavMeshAgent>();

        // ������ ����� � ��������� ������� ������������ ������������� �������
        UpdatePositionRelativeToParent();
    }

    void Update()
    {
        // �������� ������� ������� ������ �� ������ �������� �������
        agent.SetDestination(Target.position);

        // �������� ��������� ����� ������������ ������������� �������
        UpdatePositionRelativeToParent();
    }

    void UpdatePositionRelativeToParent()
    {
        if (parentObject != null)
        {
            // ������������ ����������� �� ������������� ������� � �����
            Vector3 directionFromParent = transform.position - parentObject.position;
            directionFromParent.Normalize();

            // ������������� ������� ����� �� �������� ���������� �� ������������� �������
            transform.position = parentObject.position + directionFromParent * distanceFromParent;
        }
    }
}

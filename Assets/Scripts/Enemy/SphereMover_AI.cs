using UnityEngine;
using UnityEngine.AI;

public class SphereMover_AI : MonoBehaviour
{
    public Transform Target; // Целевой объект, к которому сфера должна двигаться
    public Transform parentObject; // Родительский объект
    public float distanceFromParent = 2.0f; // Расстояние от родительского объекта
    
    private NavMeshAgent agent;

    public void Initialize(Transform target)
    {
        this.Target = target;
    }

    void Start()
    {
        // Получить NavMeshAgent компонента сферы
        agent = GetComponent<NavMeshAgent>();

        // Задать сферу в начальной позиции относительно родительского объекта
        UpdatePositionRelativeToParent();
    }

    void Update()
    {
        // Обновить целевую позицию агента на основе целевого объекта
        agent.SetDestination(Target.position);

        // Обновить положение сферы относительно родительского объекта
        UpdatePositionRelativeToParent();
    }

    void UpdatePositionRelativeToParent()
    {
        if (parentObject != null)
        {
            // Рассчитываем направление от родительского объекта к сфере
            Vector3 directionFromParent = transform.position - parentObject.position;
            directionFromParent.Normalize();

            // Устанавливаем позицию сферы на заданном расстоянии от родительского объекта
            transform.position = parentObject.position + directionFromParent * distanceFromParent;
        }
    }
}

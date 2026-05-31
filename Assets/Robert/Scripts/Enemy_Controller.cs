using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour, IDamageable
{
    [Header("Enemy Stats")]
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float health;
    [SerializeField] private float moveSpeed;

    private NavMeshAgent agent;
    private Transform playerTransform;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 8f;

    void Start()
    {
        moveSpeed = enemyData.moveSpeed;
        health = enemyData.maxHealth;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyData.moveSpeed;
        agent.updateRotation = false; // We'll handle rotation manually so it persists when stopped

        // Lock the Rigidbody to prevent physics-based pushing
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        // Cache the player reference once rather than searching every frame
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;
    }

    void Update()
    {
        if (playerTransform == null) return;

        agent.SetDestination(playerTransform.position);
        FacePlayer();
    }

    private void FacePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position);
        direction.y = 0f; // Keep rotation on the Y axis only — no tilting up/down

        if (direction.sqrMagnitude < 0.01f) return; // Already on top of player, skip

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void HandleDeath()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");

        if (health <= 0)
            HandleDeath();
    }
}
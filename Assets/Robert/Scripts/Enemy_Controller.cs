using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour, IDamageable
{

    [Header ("Enemy Stats")]
    [SerializeField] private EnemyData enemyData;

    [SerializeField] private float health;
    //[SerializeField] private int damage = enemyData;
    [SerializeField] private float moveSpeed;
    //private float last_attack_time = 0f;
    private NavMeshAgent agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = enemyData.moveSpeed;
        health = enemyData.maxHealth;

        agent = GetComponent<NavMeshAgent>();

        agent.speed = enemyData.moveSpeed;
        //agent.stoppingDistance = attack_range;
        agent.updateRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player_reference = GameObject.FindGameObjectWithTag("Player");
        if (player_reference == null) return;
        agent.SetDestination(player_reference.transform.position);
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
        {
            HandleDeath();
        }
    }
}

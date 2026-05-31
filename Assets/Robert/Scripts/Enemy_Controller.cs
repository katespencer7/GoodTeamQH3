using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{

    [Header ("Enemy Stats")]
    [SerializeField] private EnemyData enemyData;

    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private float move_speed = 3f;
    [SerializeField] private float attack_range = 1.5f;
    [SerializeField] private float attack_cooldown = 1f;
    private float last_attack_time = 0f;
    private UnityEngine.AI.NavMeshAgent agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyData = new EnemyData();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = move_speed;
        agent.stoppingDistance = attack_range;
        agent.updateRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player_reference = GameObject.FindGameObjectWithTag("Player");
        if (player_reference == null) return;
        agent.SetDestination(player_reference.transform.position);
    }
}

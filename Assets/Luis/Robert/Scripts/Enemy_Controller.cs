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

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    private AttackData attackData;
    private bool isAttacking = false;

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

        if (enemyData != null && !isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(gameObject.transform.position, playerTransform.position);
            if (distanceToPlayer <= enemyData.attackRange)
            {
                Debug.Log("Enemy is in range to attack!");
                handleAttack();
            }
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position);
        direction.y = 0f; // Keep rotation on the Y axis only — no tilting up/down

        if (direction.sqrMagnitude < 0.01f) return; // Already on top of player, skip

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void handleAttack()
    {
       
         if (isAttacking)
            return;
        Debug.Log("Enemy is attacking!");
        isAttacking = true;        
        // Play the current attack animation
        attackData = enemyData.Attack;
        int animHash = Animator.StringToHash(attackData.animationClip.name);
        animator.CrossFadeInFixedTime(animHash, 0.1f);

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


    public void OnHitboxTrigger()
    {
        HitboxUtility.Instance.CreateHitbox(
            attackData.hitboxShape,
            transform.position + transform.forward * 1f,
            transform.rotation * attackData.hitboxRotation,
            attackData.hitboxSize,
            attackData.hitboxDuration
        );
    }

        //     HitboxUtility.Instance.CreateHitbox(
        //     currentAttack.hitboxShape,
        //     currentAttack.hitboxOffset + transform.position,
        //     currentAttack.hitboxRotation,
        //     currentAttack.hitboxSize,
        //     currentAttack.hitboxDuration
        // );

    public void OnAttackAnimationEnded()
    {
        isAttacking = false;
    }

    public void OnAttackAnimationBegin()
    {
        isAttacking = true;
    }

    public void OnTriggerVFX(VFXData vfxData)
    {
        // Placeholder for triggering VFX via animation events.
        Debug.Log("Triggering VFX event!");

        VFXUtility.Instance.Play(vfxData, transform.position, transform);
    }


}
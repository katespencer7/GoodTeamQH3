using UnityEngine;

public class HitboxUtility : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;
    [SerializeField] private GameObject cubeHitboxPrefab;

    public enum HitboxShape { Cube }

    public static HitboxUtility Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CreateHitbox(
        HitboxShape shape,
        Vector3 position,
        Quaternion rotation,
        Vector3 scale,
        float duration,
        HitboxCollider.HitboxOwner owner = HitboxCollider.HitboxOwner.Player,
        float damage = 10f)
    {
        GameObject prefab = shape switch
        {
            HitboxShape.Cube => cubeHitboxPrefab,
            _ => null
        };

        if (prefab == null)
        {
            Debug.LogWarning($"HitboxUtility: No prefab assigned for shape {shape}.");
            return;
        }

        GameObject hitbox = Instantiate(prefab, position, rotation);
        hitbox.transform.localScale = scale;

        // Pass owner and damage to the collider so it knows what to hit.
        if (hitbox.TryGetComponent(out HitboxCollider col))
            col.Initialize(owner, damage);
        else
            Debug.LogWarning("HitboxUtility: Spawned hitbox prefab is missing a HitboxCollider component.");

        if (debugMode)
            Debug.Log($"[HitboxUtility] Spawned {shape} hitbox at {position} | Owner: {owner} | Damage: {damage} | Duration: {duration}s");

        Destroy(hitbox, duration);
    }
}
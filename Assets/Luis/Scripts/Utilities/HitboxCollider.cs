using System.Collections.Generic;
using UnityEngine;

public class HitboxCollider : MonoBehaviour
{
    public enum HitboxOwner { Player, Enemy }

    [Header("Settings")]
    [SerializeField] private HitboxOwner owner;
    [SerializeField] private float damage = 10f;

    // Tracks who has already been hit so a lingering hitbox
    // only applies damage once per target.
    private readonly HashSet<GameObject> alreadyHit = new();

    private void OnTriggerEnter(Collider other)
    {
        // Determine which tag this hitbox should be hitting.
        // A player-owned hitbox hits enemies and vice versa.

        string targetTag = owner == HitboxOwner.Player ? "Enemy" : "Player";

        if (!other.CompareTag(targetTag)) return;
        if (alreadyHit.Contains(other.gameObject)) return;

        alreadyHit.Add(other.gameObject);
        Hit(other.gameObject);
    }

    private void Hit(GameObject target)
    {
        Debug.Log($"[HitboxCollider] {owner} hit {target.name} for {damage} damage.");

        if (target.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(damage);
    }

    // Called by HitboxUtility when spawning so it doesn't need
    // to be set manually in the prefab Inspector.
    public void Initialize(HitboxOwner hitboxOwner, float hitboxDamage)
    {
        owner = hitboxOwner;
        damage = hitboxDamage;
    }
}
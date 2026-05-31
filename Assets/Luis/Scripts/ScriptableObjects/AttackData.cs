using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    
    [Header("Attack Info")]
    public string attackName;
    public string attackDescription;
    public float baseCooldown;
    public float forwardMovement = 1f;

    [Header("Animation Info")]
    public AnimationClip animationClip;

    [Header("Hitbox Data")]
    public HitboxUtility.HitboxShape hitboxShape;
    public Vector3 hitboxSize = Vector3.one;
    public Vector3 hitboxOffset = Vector3.zero;
    public Quaternion hitboxRotation = Quaternion.identity;
    public float hitboxDuration;

    // [Header("Visual Effects")]
    // public VFXData vfxData;

    // Cooldown state
    private float lastUsedTime = float.MinValue;

    public bool IsReady => Time.time >= lastUsedTime + baseCooldown;
    public float CooldownRemaining => Mathf.Max(0f, (lastUsedTime + baseCooldown) - Time.time);

    private void OnEnable() => lastUsedTime = float.MinValue;

    public bool TryUse()
    {
        if (!IsReady) return false;
        lastUsedTime = Time.time;
        return true;
    }
}
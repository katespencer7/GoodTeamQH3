using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{

    [Header("Attack Info")]
    public string attackName;
    public string attackDescription;

    [Header("Animation Info")]
    public AnimationClip animationClip;

    [Header("Hitbox Data")]
    public HitboxUtility.HitboxShape hitboxShape;
    public Vector3 hitboxSize = Vector3.one;
    public Vector3 hitboxOffset = Vector3.zero;
    public Quaternion hitboxRotation = Quaternion.identity;
    public float hitboxDuration;
}

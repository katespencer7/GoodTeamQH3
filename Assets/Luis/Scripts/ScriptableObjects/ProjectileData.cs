using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Stats")]
    public float speed = 15f;
    public float lifetime = 3f;
    public float damage = 10f;

    [Header("Spawn")]
    public Vector3 spawnOffset = Vector3.zero;
}
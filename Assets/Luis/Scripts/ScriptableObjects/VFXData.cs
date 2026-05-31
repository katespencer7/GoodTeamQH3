using UnityEngine;

[CreateAssetMenu(fileName = "VFXData", menuName = "Scriptable Objects/VFXData")]
public class VFXData : ScriptableObject
{
    [Header("Effect")]
    public GameObject prefab;

    [Header("Transform")]
    public Vector3 size = Vector3.one;
    public Vector3 offset = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;

    [Header("Lifetime")]
    public float lifetime = 1f;
}
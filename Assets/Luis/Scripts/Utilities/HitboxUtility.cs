using UnityEngine;

public class HitboxUtility : MonoBehaviour
{

    [SerializeField] private bool DebugMode = true;
    [SerializeField] private GameObject cubeHitboxPrefab;

    public enum HitboxShape
    {
        Cube
    }

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

    public void CreateHitbox(HitboxShape shape, Vector3 position, Quaternion rotation, Vector3 scale, float duration)
    {
        GameObject hitbox = null;

        switch (shape)
        {
            case HitboxShape.Cube:
                hitbox = Instantiate(cubeHitboxPrefab, position, rotation);
                break;
        }

        Destroy(hitbox, duration);
    }

}

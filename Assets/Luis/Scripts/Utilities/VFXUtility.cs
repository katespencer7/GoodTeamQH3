using UnityEngine;

public class VFXUtility : MonoBehaviour
{
    public static VFXUtility Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Play(VFXData data, Vector3 position, Transform parent = null)
    {
        if (data == null || data.prefab == null) return;

        GameObject vfx = Instantiate(
            data.prefab,
            position + data.offset,
            data.rotation,
            parent
        );

        vfx.transform.localScale = data.size;
        Destroy(vfx, data.lifetime);
    }
}
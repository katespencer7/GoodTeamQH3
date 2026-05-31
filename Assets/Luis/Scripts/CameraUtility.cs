using UnityEngine;

public class CameraUtility : MonoBehaviour
{
    public static CameraUtility Instance { get; private set; }
    public Camera Camera { get; private set; }

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow")]
    [SerializeField] private float followSpeed = 8f;

    private Transform camTransform;
    private Vector3 offset;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Camera = Camera.main;

        if (Camera != null)
            camTransform = Camera.transform;
        else
            Debug.LogWarning("CameraUtility: No camera tagged 'MainCamera' was found.");
    }

    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
                target = player.transform;
            else
                Debug.LogWarning("CameraUtility: No target assigned and no GameObject with tag 'Player' was found.");
        }

        if (target != null && camTransform != null)
            offset = camTransform.position - target.position;
    }

    private void LateUpdate()
    {
        if (target == null || camTransform == null) return;

        Vector3 targetPosition = target.position + offset;

        camTransform.position = Vector3.Lerp(
            camTransform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (camTransform != null)
            offset = camTransform.position - target.position;
    }
}
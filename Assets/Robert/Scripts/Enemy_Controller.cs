using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{

    [Header ("Enemy Stats")]
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private float move_speed = 3f;
    [SerializeField] private float attack_range = 1.5f;
    [SerializeField] private float attack_cooldown = 1f;
    private float last_attack_time = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

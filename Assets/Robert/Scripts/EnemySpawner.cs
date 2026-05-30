using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    [Header ("Spawning")]
    [SerializeField] private int enemies_per_round = 5;
    [SerializeField] private int enemies_left = 0;
    [SerializeField] private float spawn_delay = 2f;
    [SerializeField] private int round_count = 0;
    [SerializeField] private int room_rounds = 3;
    [SerializeField] private GameObject[] spawnPoints = new GameObject[4];
    [SerializeField] private GameObject[] enemies;
    private bool round_active = false;
    private bool room_cleared = false;
    private bool round_complete = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       SpawnEnemies();
    }

    void SpawnEnemies()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomIndex], spawnPoint.transform.position, Quaternion.identity);
        }
        enemies_left = enemies_per_round;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies_left <= 0)
        {
            round_count++;
            round_complete = true;

            if (round_count >= room_rounds)
            {        
                room_cleared = true;

            }

            round_active = false;
        }
            if (!round_active && !room_cleared && round_complete)
            {
                StartCoroutine(WaitAndSpawn());
            }
    }

    private IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(spawn_delay);
        SpawnEnemies();
        round_active = true;
    }
}

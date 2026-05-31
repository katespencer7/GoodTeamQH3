using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    [Header ("Spawning")]
    [SerializeField] private int enemies_per_round = 10;
    [SerializeField] public int enemies_left = 0;
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
        enemies_left = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomIndex], spawnPoint.transform.position, Quaternion.identity);
            enemies_left++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies_left = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemies_left <= 0 && !round_active && !room_cleared && !round_complete)
        {
            round_count++;
            round_complete = true;
            enemies_per_round += Mathf.FloorToInt(round_count * 1.5f); 

            if (round_count >= room_rounds)
            {
                room_cleared = true;
                Debug.Log("Room Cleared!");
                return;
            }

            Debug.Log("Round Complete! Starting next round...");
            round_active = true;
            StartCoroutine(WaitAndSpawn());
        }
    }

    private IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(spawn_delay);
        Debug.Log("Spawning next round of enemies...");
        SpawnEnemies();
        round_complete = false;
        round_active = false;
    }
}

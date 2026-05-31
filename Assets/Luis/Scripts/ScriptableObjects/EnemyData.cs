using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string enemyDescription;
    public Sprite enemySprite;

    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
}

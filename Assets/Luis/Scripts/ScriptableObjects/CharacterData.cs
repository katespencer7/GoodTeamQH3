using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Character Info")]
    public string character_name;

    [Header("Character Stats")]
    public int max_health;
    
    [Header("Light Attacks")]
    public AttackData attackData1;

    [Header("Heavy Attacks")]
    public AttackData attackData2;

    [Header("Skills")]
    public AttackData skillData1;

}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Character Info")]
    public string character_name;

    [Header("Character Stats")]
    public int max_health;
    
    [Header("Light Attacks")]
    public List<AttackData> lightAttacks;

    [Header("Heavy Attacks")]
    public List<AttackData> heavyAttacks;

    [Header("Skills")]
    public AttackData skillData1;
    public AttackData skillData2;
    public AttackData skillData3;
    public AttackData skillData4;

}

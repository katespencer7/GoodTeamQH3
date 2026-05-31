using UnityEngine;
using TMPro;

public class HUDUtility : MonoBehaviour
{
    public static HUDUtility Instance { get; private set; }

    [SerializeField]private TMP_Text skill1CooldownText;
    [SerializeField]private TMP_Text skill2CooldownText;
    [SerializeField]private TMP_Text skill3CooldownText;
    [SerializeField]private TMP_Text skill4CooldownText;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void RetrievePlayerStats(CharacterData characterData)
    {
        ManageCooldownText(characterData.skillData1.CooldownRemaining,
            characterData.skillData2.CooldownRemaining,
            characterData.skillData3.CooldownRemaining,
            characterData.skillData4.CooldownRemaining);
    }

    public void ManageCooldownText(float skill1Cooldown, float skill2Cooldown, float skill3Cooldown, float skill4Cooldown)
    {
        skill1CooldownText.text = skill1Cooldown > 0 ? skill1Cooldown.ToString("F1") : "";
        skill2CooldownText.text = skill2Cooldown > 0 ? skill2Cooldown.ToString("F1") : "";
        skill3CooldownText.text = skill3Cooldown > 0 ? skill3Cooldown.ToString("F1") : "";
        skill4CooldownText.text = skill4Cooldown > 0 ? skill4Cooldown.ToString("F1") : "";
    }



    private void Update()
    {
    

    }
}

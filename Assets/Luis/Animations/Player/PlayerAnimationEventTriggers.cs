using UnityEngine;

public class PlayerAnimationEventTriggers : MonoBehaviour
{

    private PlayerMovement player;

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerMovement>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnHitboxTriggerEvent()
    {
        player.OnHitboxTrigger();
    }

    public void OnAttackAnimationBeginEvent()
    {
        player.OnAttackAnimationBegin();
    }

    public void OnAttackAnimationEndedEvent()
    {
        player.OnAttackAnimationEnded();
    }

    public void OnTriggerVFXEvent(VFXData vfxData)
    {
        player.OnTriggerVFX(vfxData);
    }
}

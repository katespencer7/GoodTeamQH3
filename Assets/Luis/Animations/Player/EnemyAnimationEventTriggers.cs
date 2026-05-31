using UnityEngine;

public class EnemyAnimationEventTriggers : MonoBehaviour
{
    private Enemy_Controller enemyController;

    private void Start()
    {
        enemyController = transform.parent.GetComponent<Enemy_Controller>();
    }

    public void OnHitboxTriggerEvent()
    {
        enemyController.OnHitboxTrigger();
    }

    public void OnAttackAnimationBeginEvent()
    {
        enemyController.OnAttackAnimationBegin();
    }

    public void OnAttackAnimationEndedEvent()
    {
        enemyController.OnAttackAnimationEnded();
    }

    public void OnTriggerVFXEvent(VFXData vfxData)
    {
        enemyController.OnTriggerVFX(vfxData);
    }
}

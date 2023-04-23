using UnityEngine;


[RequireComponent(typeof(Collider))]
public class UnitCollisionController : MonoBehaviour
{
    public event System.Action CollidedWithGateEvent;
    public event System.Action CollidedWithSpikeEvent;
    public event System.Action<BossController, int> CollidedWithBossEvent;
    public event System.Action<EnemyController, int> CollidedWithEnemyEvent;

    private void OnTriggerEnter(Collider other)
    {
        /////////////--------- Collisions effect on root object (RootCollisionManager)---------------////////////
        if (other.transform.TryGetComponent(out CollectibleUnit collectibleUnit))
        {
            if (collectibleUnit.IsAlreadyCollected()) return;

            RootCollisionManager.instance.TriggerUnitCollectedEvent(collectibleUnit);
            collectibleUnit.GetCollected();
        }


        ///////////------- Collisions effect on itself (CollectibleUnit)--------//////////
        if (other.TryGetComponent(out GateController gate))
        {
            CollidedWithGateEvent?.Invoke();
        }

        if (other.TryGetComponent(out EnemyController enemy))
        {
            if (!enemy.canInteract) return;
            CollidedWithEnemyEvent?.Invoke(enemy, enemy.GetEnemyLevel());
        }

        if (other.TryGetComponent(out SpikeController spike))
        {
            CollidedWithSpikeEvent?.Invoke();
        }

        if (other.TryGetComponent(out BossController bossController))
        {
            CollidedWithBossEvent?.Invoke(bossController,bossController.GetBossLevel());
        }
    }

}

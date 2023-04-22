using UnityEngine;


[RequireComponent(typeof(Collider))]
public class UnitCollisionController : MonoBehaviour
{
    public event System.Action CollidedWithGateEvent;
    public event System.Action<EnemyController, int> CollidedWithEnemyEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GateController gate))
        {
            CollidedWithGateEvent?.Invoke();
        }

        if (other.TryGetComponent(out EnemyController enemy))
        {
            if (!enemy.canInteract) return;
            CollidedWithEnemyEvent?.Invoke(enemy, enemy.GetEnemyLevel());
        }
    }

}

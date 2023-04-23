using UnityEngine;

public class RootCollisionManager : SingletonBehavior<RootCollisionManager>
{

    public delegate void UnitCollectedEventDelegate(CollectibleUnit unit);
    public event UnitCollectedEventDelegate UnitCollectedEvent;

    public event System.Action CollidedWithFinishLineEvent;


    [Header("References")]
    public Transform stackRoot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectibleUnit collectibleUnit))
        {
            if (collectibleUnit.IsAlreadyCollected()) return;

            collectibleUnit.GetCollected();
            TriggerUnitCollectedEvent(collectibleUnit);
        }    
        if (other.transform.TryGetComponent(out FinishLineController finishLine))
        {
            CollidedWithFinishLineEvent?.Invoke();
        }
    }


    public void TriggerUnitCollectedEvent(CollectibleUnit collectibleUnit)
    {
        UnitCollectedEvent?.Invoke(collectibleUnit);
    }

}

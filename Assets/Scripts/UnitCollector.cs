using UnityEngine;

public class UnitCollector : SingletonBehavior<UnitCollector>
{

    public delegate void UnitCollectedEventDelegate(CollectibleUnit unit);
    public event UnitCollectedEventDelegate UnitCollectedEvent;

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
    }


    public void TriggerUnitCollectedEvent(CollectibleUnit collectibleUnit)
    {
        UnitCollectedEvent?.Invoke(collectibleUnit);
    }

}

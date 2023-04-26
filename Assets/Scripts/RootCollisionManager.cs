using UnityEngine;

public class RootCollisionManager : MonoBehaviour
{
    public static RootCollisionManager instance;

    public delegate void UnitCollectedEventDelegate(CollectibleUnit unit);
    public event UnitCollectedEventDelegate UnitCollectedEvent;

    public event System.Action CollidedWithFinishLineEvent;
    public event System.Action GameIsFailedSomehowEvent;


    [Header("References")]
    public Transform stackRoot;

    [Header("Debug")]
    Collider _collider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _collider = GetComponent<Collider>();
    }

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
        if (other.transform.TryGetComponent(out EnemyController enemy))
        {
            Failed();
        }
        if (other.transform.TryGetComponent(out BossController boss))
        {
            Failed();
        }
        if (other.transform.TryGetComponent(out SpikeController spike))
        {
            Failed();
        }
    }

    void Failed()
    {
        _collider.enabled = false;

        GameIsFailedSomehowEvent?.Invoke();

        GameManager.instance.EndGame(false);
    }

    public void TriggerUnitCollectedEvent(CollectibleUnit collectibleUnit)
    {
        UnitCollectedEvent?.Invoke(collectibleUnit);
    }


}

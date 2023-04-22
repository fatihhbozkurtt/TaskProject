using UnityEngine;

public class CollectibleUnit : MonoBehaviour
{
    public event System.Action<int> UnitLevelChangedEvent;

    [Header("Configuration")]
    [SerializeField] int unitLevel;

    [Header("References")]
    [SerializeField] Material[] materials;

    [Header("Debug")]
    [SerializeField] bool isCollected;
    public static Vector3 collectibleScale;
    UnitCollisionController collisionController;
    const int maxUnitLevel = 3;

    private void Awake()
    {
        collectibleScale = transform.localScale;
        collisionController = GetComponent<UnitCollisionController>();

    }
    private void Start()
    {
        collisionController.CollidedWithGateEvent += OnCollidedWithGate;
        collisionController.CollidedWithEnemyEvent += OnCollidedWithEnemy;
    }

    #region Collision Event Subscribers

    private void OnCollidedWithGate()
    {
        if (unitLevel == maxUnitLevel)
        {
            Debug.Log("Maximum unit level is reached"); return;
        }

        unitLevel++;
        UnitLevelChangedEvent?.Invoke(unitLevel);

    }
    private void OnCollidedWithEnemy(EnemyController enemy, int enemyLevel)
    {
        if (unitLevel > enemyLevel)
        {
            unitLevel -= enemyLevel;
            enemy.SetEnemyLevel(-1);// -1 means enemy should destroy yourself
            UnitLevelChangedEvent?.Invoke(unitLevel);

        }
        else if (unitLevel < enemyLevel)
        {
            enemyLevel -= unitLevel;
            enemy.SetEnemyLevel(enemyLevel);
            DestroySelf();

        }
        else if (unitLevel == enemyLevel)
        {
            DestroySelf();
            enemy.SetEnemyLevel(-1);

        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out CollectibleUnit collectibleUnit))
        {
            if (collectibleUnit.IsAlreadyCollected()) return;

            UnitCollector.instance.TriggerUnitCollectedEvent(collectibleUnit);
            collectibleUnit.GetCollected();
        }
    }

    void DestroySelf()
    {
        StackManager.instance.RemoveUnitFromList(transform);
        Destroy(gameObject, .1f);
    }
    public int GetUnitLevel()
    {
        return unitLevel;
    }

    public void GetCollected()
    {
        isCollected = true;
    }

    public bool IsAlreadyCollected()
    {
        return isCollected;
    }
}

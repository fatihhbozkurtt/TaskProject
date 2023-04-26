using DG.Tweening;
using UnityEngine;

public class CollectibleUnit : MonoBehaviour
{
    public event System.Action<int> UnitLevelChangedEvent;

    [Header("Configuration")]
    [SerializeField] int unitLevel;

    //[Header("References")]
    Collider _collider;

    [Header("Debug")]
    [SerializeField] bool isCollected;
    public static Vector3 collectibleScale;
    UnitCollisionController collisionController;
    const int maxUnitLevel = 3;

    private void Awake()
    {
        collectibleScale = transform.localScale;
        collisionController = GetComponent<UnitCollisionController>();
        _collider = GetComponent<Collider>();
    }
    private void Start()
    {
        collisionController.CollidedWithGateEvent += OnCollidedWithGate;
        collisionController.CollidedWithEnemyEvent += OnCollidedWithEnemy;
        collisionController.CollidedWithSpikeEvent += OnCollidedSpike;
        collisionController.CollidedWithBossEvent += OnCollidedBoss;
    }


    #region Collision Event Listeners

    private void OnCollidedWithGate()
    {
        if (unitLevel == maxUnitLevel)
        {
            Debug.Log("Maximum unit level is reached"); return;
        }

        unitLevel++;
        UnitLevelChangedEvent?.Invoke(unitLevel);

    }
    private void OnCollidedBoss(BossController bossController, int bossLevel)
    {
        if (unitLevel > bossLevel)
        {
            unitLevel -= bossLevel;
            UnitLevelChangedEvent?.Invoke(unitLevel);
        }
        else if (unitLevel < bossLevel)
        {
            _collider.enabled = false;
            DestroySelf();

   
        }
        else if (unitLevel == bossLevel)
        {
            _collider.enabled = false;
            DestroySelf();
      
        }

        bossController.CollidedWithUnit(unitLevel); // In any case boss should be informed about collision for  private implementation(scale down, level text updating)
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
    private void OnCollidedSpike()
    {
        DestroySelf();
    }
    #endregion

    void DestroySelf()
    {
        StackManager.instance.RemoveUnitFromList(transform);
        transform.DOKill();
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

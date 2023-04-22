using UnityEngine;

public class EnemyMeshAssigner : BaseMeshAssigner
{
    [Header("Debug")]
    [SerializeField] EnemyController enemyController;
    private void Awake()
    {
        enemyController = transform.parent.GetComponent<EnemyController>(); 
    }
    private void Start()
    {
        AssignMeshByLevel(enemyController.GetEnemyLevel());
    }


}

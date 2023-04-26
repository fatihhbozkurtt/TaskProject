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
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if(i == enemyController.GetEnemyLevel() - 1)
            {
                child.SetActive(true);
            }
            else
                Destroy(child);



        }
    }


}

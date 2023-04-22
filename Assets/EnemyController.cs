using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("If the value equals to -1, it means the enemy is not active to interact and must be destroyed.")]
    [SerializeField] int enemyLevel;
    public bool canInteract = true;
    public int GetEnemyLevel()
    {
        return enemyLevel;
    }

    public void SetEnemyLevel(int updatedLevel)
    {
        if (updatedLevel == -1) 
        { 
            canInteract = false;
            DestroySelf(); 
            return;
        }

        enemyLevel = updatedLevel;
    }

    private void DestroySelf()
    {

        Destroy(gameObject, .1f);
    }
}

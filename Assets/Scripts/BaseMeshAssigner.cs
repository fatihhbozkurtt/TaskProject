using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMeshAssigner : MonoBehaviour
{
    [Header("References")]
    public GameObject[] meshes;


    private void Start()
    {
        AssignMeshByLevel();

    }

    public void AssignMeshByLevel(int level = 0)
    {
        DeactivateAllMeshes();

        for (int i = 0; i < meshes.Length; i++)
        {
            if (i == level - 1) meshes[i].SetActive(true);
        }
    }

    public void DeactivateAllMeshes()
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].SetActive(false);
        }
    }

}

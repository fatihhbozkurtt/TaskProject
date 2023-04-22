using UnityEngine;

public class UnitMeshAssigner : BaseMeshAssigner
{
    [Header("Debug")]
    [SerializeField] CollectibleUnit unit;

    private void Awake()
    {
        unit = transform.parent.GetComponent<CollectibleUnit>();
        unit.UnitLevelChangedEvent += OnUnitLevelChanged;
    }
    private void Start()
    {
        AssignMeshByLevel(unit.GetUnitLevel());
    }

    private void OnUnitLevelChanged(int unitLevel)
    {
        AssignMeshByLevel(unitLevel);
    }
}
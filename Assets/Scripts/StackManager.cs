using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManager : SingletonBehavior<StackManager>
{
    [Header("Configuration")]
    public float offsetBetweenUnits;
    [Tooltip("Offset between root and first collected collectibleUnit")]
    [SerializeField] float initialOffset;
    [Tooltip("Horizontal swing movement, the greater the value the quicker swing movement acquired")]
    [SerializeField] float swingRatio;

    [Header("Debug")]
    [SerializeField] List<Transform> StackList;
    [SerializeField] Transform stackRoot;
    [SerializeField] Transform lastCollectedUnit;
    List<Vector3> displacements;

    private void Start()
    {
        stackRoot = UnitCollector.instance.stackRoot;
        UnitCollector.instance.UnitCollectedEvent += OnUnitCollected;
        displacements = new List<Vector3>();
    }

    private void Update()
    {
        SwingMovement();
    }


    private void OnUnitCollected(CollectibleUnit unit)
    {
        AddUnitToList(unit.transform);
        displacements.Add(Vector3.zero);
        lastCollectedUnit = unit.transform;


        StartCoroutine(ScalingRoutine());
    }

    void SwingMovement()
    {
        Vector3 stackPos = stackRoot.position;
        Vector3 targetPos = Vector3.zero;

        for (int i = 0; i < StackList.Count; i++)
        {

            #region Target Pos Assigning
            if (i == 0)
            {
                targetPos = stackPos + (Vector3.forward * initialOffset); // Ýlk stacklenen nesnenin stackparent a olan uzaklýðý

            }
            else
            {
                Vector3 lastCollectedUnit = StackList[i - 1].position;
                targetPos = lastCollectedUnit + (offsetBetweenUnits * Vector3.forward); // Ýlk nesneden sonra stacklenen nesnelerin previous nesneye uzaklýðý
            }
            #endregion

            Transform collectibleUnit = StackList[i];
            Vector3 displacement = swingRatio * Time.deltaTime * (targetPos - collectibleUnit.position);

            displacement.y = 0; // Salýným olayýnda y ekseni sabit kalýr
            displacement.z = 0; // Salýným olayýnda z ekseni sabit kalýr

            Vector3 pos = collectibleUnit.position + displacement; // Unitlerin positionlarýný yer deðiþtirmeye eþitlemek
            pos.z = targetPos.z;

            collectibleUnit.position = pos;
            displacements[i] = displacement;
        }
    }

    IEnumerator ScalingRoutine()
    {
        for (int i = 1; i <= StackList.Count; i++)
        {
            Transform unitTr = StackList[StackList.Count - i].transform;
            Vector3 unitScale = CollectibleUnit.collectibleScale;

            unitTr.DOScale(unitScale * 1.35f, 0.15f).From(unitScale).OnComplete(() =>
            {
                unitTr.DOScale(unitScale, 0.15f);
            });

            yield return new WaitForSeconds(0.065f);
        }

    }
    public int GetStackCount()
    {
        return StackList.Count;
    }
    public void AddUnitToList(Transform unit)
    {
        StackList.Add(unit);
    }

    public void RemoveUnitFromList(Transform unit)
    {
        StackList.Remove(unit);  
    }
}

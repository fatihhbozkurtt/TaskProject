
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Coonfiguration")]
    [SerializeField] int bossLevel;
    public Vector3 MinBossScale, MaxBossScale;

    [Header("References")]
    [SerializeField] Vector3 scaleRatio;
    [SerializeField] TextMeshProUGUI levelText;

    BoxCollider _collider;
    Vector3 initialPos;

    private void Start()
    {
 
        _collider = GetComponent<BoxCollider>();
        UpdateLevelText();

        scaleRatio = (MaxBossScale - MinBossScale) / bossLevel;
    }

    public int GetBossLevel()
    {
        return bossLevel;
    }

    public void CollidedWithUnit(int collidedUnitLevel)
    {
        if (collidedUnitLevel >= bossLevel)
        {
            GameManager.instance.EndGame(true);// Level completed successfully
            _collider.enabled = false; // NO interaction needed after here
            DestroyingSelf();
            return;
        }

        bossLevel -= collidedUnitLevel;
        UpdateLevelText();
        GraduallyScaleDown(collidedUnitLevel);
    }

    public void GraduallyScaleDown(int collidedUnitLevel)
    {
        Vector3 decrementAmount =  scaleRatio * collidedUnitLevel;
        Vector3 newScale =  transform.localScale - decrementAmount;

        transform.DOScale(newScale, .5f);
 
    }
    private void UpdateLevelText()
    {
        levelText.text = bossLevel.ToString();
    }
    void DestroyingSelf()
    {

        transform.DOScale(Vector3.zero, .75f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}

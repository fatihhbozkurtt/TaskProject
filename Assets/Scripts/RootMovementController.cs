using UnityEngine;

public class RootMovementController : MonoBehaviour
{
    [Header("Configuration")]   // Fields  that can be manuplated from editor go here
    public float forwardSpeed;
    public float horizontalSpeed;
    [SerializeField] Vector2 horizontalBorders;

    [Header("Debug")]    // Fields that need to be seen on editor go here
    [SerializeField] Vector3 firstMousePos;
    [SerializeField] Vector3 lastMousePos;

    [SerializeField] bool canPerformMovement;
    [SerializeField] bool stopHorizontalMovement;
    private void Awake()
    {
        canPerformMovement = true;

        RootCollisionManager.instance.CollidedWithFinishLineEvent += OnCollidedFinishLine;
        RootCollisionManager.instance.CollidedWithAnyTypeOfEnemyEvent += OnCollidedAnyEnemy; ;

    }

    private void OnCollidedAnyEnemy()
    {
        StopHorizontalMovement();
        StopVerticalMovement();
    }
    private void OnCollidedFinishLine()
    {
        StopHorizontalMovement();
    }

    private void Update()
    {

        if (!canPerformMovement) return;
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        if (stopHorizontalMovement) return;
        Vector3 swerveDelta = CalculateHorizontalDelta();
        transform.Translate(swerveDelta.x * horizontalSpeed * Time.deltaTime, 0, 0);


        ClampHorizontolMovement();
    }


    Vector3 CalculateHorizontalDelta()
    {

        Vector3 swerveDelta = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePos.x = Input.mousePosition.x / Screen.width;
        }
        if (Input.GetMouseButton(0))
        {
            lastMousePos.x = Input.mousePosition.x / Screen.width;
            swerveDelta = lastMousePos - firstMousePos;
            firstMousePos = lastMousePos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastMousePos = Vector3.zero;
            firstMousePos = Vector3.zero;
            swerveDelta = Vector3.zero;
            swerveDelta = lastMousePos - firstMousePos;
        }

        return swerveDelta;
    }


    void ClampHorizontolMovement()
    {
        if (transform.position.x > horizontalBorders.x)
        {
            gameObject.transform.position = new Vector3(horizontalBorders.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x < horizontalBorders.y)
        {
            gameObject.transform.position = new Vector3(horizontalBorders.y, transform.position.y, transform.position.z);
        }
    }


    public void StopVerticalMovement()
    {
        canPerformMovement = false;
    }

    public void StopHorizontalMovement()
    {
        stopHorizontalMovement = true;
    }
}

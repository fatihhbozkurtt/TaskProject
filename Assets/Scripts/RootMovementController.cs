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
    [SerializeField] Vector3 swerveDelta;
    [SerializeField] bool canPerformMovement;
    private void Awake()
    {
        canPerformMovement = true;
    }
    private void Update()
    {


        if (!canPerformMovement) return;

        CalculateHorizontalDelta();

        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        transform.Translate(swerveDelta.x * horizontalSpeed * Time.deltaTime, 0, 0);


        ClampHorizontolMovement();
    }


    void CalculateHorizontalDelta()
    {
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


    public void StopMovement()
    {
        canPerformMovement = false;
    }


}

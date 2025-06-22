using UnityEngine;
public class CameraController : MonoBehaviour
{
    public float SensX;
    public float SensY;

    [SerializeField] private Transform orientation;

    private float xRotaion;
    private float yRotaion;

    [SerializeField] private Transform cameraPosition;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = cameraPosition.position;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        yRotaion += mouseX;
        xRotaion -= mouseY;
        xRotaion = Mathf.Clamp(xRotaion, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotaion, yRotaion, 0);
        orientation.rotation = Quaternion.Euler(0, yRotaion, 0);
    }
}

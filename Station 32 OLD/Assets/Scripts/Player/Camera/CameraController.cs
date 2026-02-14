using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float SensX;
    public float SensY;

    private float _xRotaion;
    private float _yRotaion;

    [SerializeField] private Transform _playerBody;

    [SerializeField] private Transform _cameraPosition;

    private void Update()
    {
        transform.position = _cameraPosition.position;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        _yRotaion += mouseX;
        _xRotaion -= mouseY;
        _xRotaion = Mathf.Clamp(_xRotaion, -90f, 90f);

        transform.rotation = Quaternion.Euler(_xRotaion, _yRotaion, 0);
        _playerBody.rotation = Quaternion.Euler(0, _yRotaion, 0);
    }
}
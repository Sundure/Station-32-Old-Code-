using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _bodyTransform;

    [SerializeField] private float _moveSpeed;
    
    [SerializeField] private LayerMask _groundLayer;

    private void Update()
    {
        float xSpeed = Input.GetAxis("Horizontal");
        float ySpeed = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(xSpeed * _moveSpeed, 0, ySpeed * _moveSpeed);
        
        _characterController.Move(moveDirection * Time.deltaTime);
    }
}

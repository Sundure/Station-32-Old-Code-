using UnityEngine;

public class Ventcheck : MonoBehaviour
{
    public int BoltsCount = 2;
    [SerializeField] private Rigidbody _rb; 
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void BoltsCheck()
    {
        if (BoltsCount <= 0)
        {
            _rb.isKinematic = false;
        }
    }
}

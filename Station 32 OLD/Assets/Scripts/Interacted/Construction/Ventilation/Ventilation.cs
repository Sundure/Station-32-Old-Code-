using UnityEngine;

public class Ventilation : MonoBehaviour
{
    [HideInInspector] public int BoltsCount;

    [SerializeField] private Rigidbody _rigidbody;

    public void UnscrewBolt()
    {
        BoltsCount--;

        if (BoltsCount <= 0)
            _rigidbody.isKinematic = false;
    }
}

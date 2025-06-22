using UnityEngine;

public class Ventboltcheck : MonoBehaviour
{
    [SerializeField] private Ventcheck _ventchek;
    public void VentDestroy()
    {
        _ventchek.BoltsCount--;
        Destroy(gameObject);
        _ventchek.BoltsCheck();
    }
}

using UnityEngine;

public class TimeToLeaveHint : MonoBehaviour
{
    [SerializeField] private GameObject _hint;

    private void Awake()
    {
        DoorTimer.TimeOver += ShowHint;

        gameObject.SetActive(false);
    }

    private void ShowHint()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        DoorTimer.TimeOver -= ShowHint;
    }
}

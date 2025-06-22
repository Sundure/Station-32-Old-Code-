using UnityEngine;
public class InteractSystem : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;

    [SerializeField] private GameObject _interactTip;

    private readonly float _interactRange = 3f;

    private readonly float _updateInterval = 0.1f; // Interact Update Interval For Optimization

    private float _updateFill; //Dump Name

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit, _interactRange))
            {
                if (hit.collider.TryGetComponent(out Interacted interacted) && interacted.InteractCondition())
                {
                    interacted.Interact();
                }
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {

        }

        _updateFill += Time.deltaTime;

        if (_updateFill >= _updateInterval)
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit, _interactRange))
            {
                if (hit.collider.TryGetComponent(out Interacted interacted) && interacted.InteractCondition())
                    _interactTip.SetActive(true);
                else
                    _interactTip.SetActive(false);
            }
            else
                _interactTip.SetActive(false);
        }
    }
}

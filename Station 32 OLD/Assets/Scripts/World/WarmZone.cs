using UnityEngine;

public class WarmZone : MonoBehaviour  // Need To Add Custom Inspector Component For This
{
    [SerializeField] private Stove _stove;

    [SerializeField] private Transform _playerTransform;

    [SerializeField] private BoxCollider _collider;

    [SerializeField] private float _minDistance = 2.5f;
    [SerializeField] private float _maxDistance;

    private void Awake()
    {
        if (_collider.isTrigger == false)
            _collider.isTrigger = true;

        if (_playerTransform == null)
        {
            Debug.LogError("PlayerTransform = Null");
        }

        _maxDistance = _collider.size.z > _collider.size.x ? _collider.size.z : _collider.size.x;

        enabled = false;
    }

    private void Update()
    {
        if (_stove.Burn)
        {
            float distance = Vector3.Distance(_stove.transform.position, _playerTransform.position);

            if (distance <= _maxDistance)
            {
                float range = _maxDistance - _minDistance;

                float distanceNormalized = ((distance - _minDistance) / range) - 1;
                distanceNormalized = Mathf.Abs(distanceNormalized);
                distanceNormalized = Mathf.Clamp01(distanceNormalized);

                float warmStrenght = _stove.WarmStrength * distanceNormalized;

                Player.Instance.Temperature += warmStrenght;
            }
        }
        else
            enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enabled = false;
        }
    }
}

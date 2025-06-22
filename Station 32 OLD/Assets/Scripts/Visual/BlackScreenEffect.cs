using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenEffect : MonoBehaviour
{
    [SerializeField] private Image _blackScreen;

    [SerializeField] private float _liveTime = 3;

    private float _startLifeTime;

    private void Awake()
    {
        _startLifeTime = _liveTime;

        StartCoroutine(DestroyTimer());
    }

    private void Update()
    {
        Color curentlyColor = _blackScreen.color;
        curentlyColor.a = _liveTime / _startLifeTime;
        _blackScreen.color = curentlyColor;

        _liveTime -= Time.deltaTime;
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(_startLifeTime);

        Destroy(gameObject);
    }
}

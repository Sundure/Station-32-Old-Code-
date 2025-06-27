using System;
using System.Collections;
using UnityEngine;

public class VideoClipDestroyer : MonoBehaviour
{
    [SerializeField] private VideoController _videoController;

    public event Action OnDestroyed;

    private void Start()
    {
        StartCoroutine(DestroyVideoClip((float)_videoController.VideoClip.length));
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            OnDestroyed?.Invoke();
            Destroy(_videoController.gameObject);
        }
    }

    private IEnumerator DestroyVideoClip(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        OnDestroyed?.Invoke();
        Destroy(_videoController.gameObject);
    }
}

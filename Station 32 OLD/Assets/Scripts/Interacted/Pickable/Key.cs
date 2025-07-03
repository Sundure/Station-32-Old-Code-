using UnityEngine;

public class Key : Interacted
{
    [SerializeField] private AudioClip _pickUpAudioClip;

    protected override void Use()
    {
        Player.Instance.Key = true;

        PlayerAudioManager.PlayOneShoot(_pickUpAudioClip);

        Destroy(gameObject);
    }
}

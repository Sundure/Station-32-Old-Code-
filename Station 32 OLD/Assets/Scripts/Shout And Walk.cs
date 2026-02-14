using System.Threading.Tasks;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Animator _shoutAnim;
    [SerializeField] private MonsterAI _monsterAI;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioStomp;
    [SerializeField] private AudioClip[] _audioShoutArray = new AudioClip[2];
    
    private void Awake() => _audioSource.clip = _audioStomp;
    
    public void StompAudio()
    {
        if (_monsterAI.CanShout)
        {
            _audioSource.Play();
        }
    }

    public async void Shout()
    {
        int randomIndex = Random.Range(0, _audioShoutArray.Length);
        _audioSource.clip = _audioShoutArray[randomIndex];
        _audioSource.Play();
        _shoutAnim.SetBool("Shout", true);
        await Task.Delay(2180);
        _shoutAnim.SetBool("Shout", false);
        StompAudio();
        _audioSource.clip = _audioStomp;
        _audioSource.Play();
    }
}

using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour                                                 
{                                                                                      
    [SerializeField] private Transform _target;                                        
    public GameObject _player;                                                         
    [SerializeField] private Transform[] _patrolingPointArray = new Transform[8];      
    private NavMeshAgent _agent;                                                       
    private Vector3 _magnituda;                                                        
    private Vector3 _magnitudaToTarget;                                                
    public float MetersToPlayer;                                                       
    private float _metersToTarget;                                                     
    [SerializeField] private bool _patroling;                                          
                                                                                       
    [SerializeField] private float _viewRadius;                                        
    [SerializeField] private float _viewAngle;                                         
                                                                                       
    [SerializeField] private LayerMask _obstacle;                                      
                                                                                       
    public bool _isSee;                                                                
                                                                                       
    [SerializeField] private ShoutAndWalkMechanic _shoutAndWalkMechanic;
    private float _shoutTime;
    public bool CanShout;
    [SerializeField] private CameraController _cameraScript;

    public AudioSource AudioSourceScreamer;
    [SerializeField] private AudioClip[] _audioArray = new AudioClip[2];
    private bool _canPlayAudio;

    [SerializeField] private Playermovment _playerMovet;

    [SerializeField] private ExitDoor _exitDoorScript;

    [SerializeField] private Transform _pastPlayerPosition;
    private Vector3 _magnitudaToPastPlayerPosition;
    [SerializeField] private float _metersToPastTargetPosition;
    [SerializeField] private bool _search;
    [SerializeField] private bool _searchFind;
    [SerializeField] private GameObject _searchPath;
    private float _searchTime;

    private void Awake()
    {
        ExitDoor.OnPlayerWin += Disable;

        AudioSourceScreamer = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
        Patroling();
    }

    void Update()
    {
        Vector3 vector3 = _player.transform.position;
        _magnituda = vector3 - transform.position;
        MetersToPlayer = _magnituda.magnitude;
        Vector3 _playerTarget = (_player.transform.position - transform.position).normalized;


        if (_patroling == true)
        {
            Vector3 vector3Target = _target.transform.position;
            _magnitudaToTarget = vector3Target - transform.position;
            _metersToTarget = _magnitudaToTarget.magnitude;
        }

        if (Vector3.Angle(transform.forward, _playerTarget) < _viewAngle / 2)
        {
            if (MetersToPlayer <= _viewRadius)
            {
                if (Physics.Raycast(transform.position, _playerTarget, MetersToPlayer, _obstacle) == false)
                {
                    _isSee = true;
                    _search = false;
                }
                else if (_isSee == true)
                {
                    _search = true;
                }
            }
        }
        if (_patroling == true)
        {
            _agent.destination = _target.position;
        }
        if (_isSee == true)
        {
            if (_search == false)
            {
                _pastPlayerPosition = _player.transform;
                _searchFind = false;
            }
            _patroling = false;
            if (CanShout == true)
            {
                _shoutAndWalkMechanic.Shout();
                CanShout = false;
            }
            _shoutTime += Time.deltaTime;
            if (_searchFind == false)
            {
                _target = _player.transform;
            }
            _agent.speed = 0f;
            _viewRadius = 30f;

            if (_shoutTime > 2.18f)
            {
                _shoutAndWalkMechanic.StompAudio();

                if (_search == true)
                {
                    if (_searchFind == false)
                    {
                        Vector3 vector3PastPlayerPosition = _pastPlayerPosition.transform.position;
                        _magnitudaToPastPlayerPosition = vector3PastPlayerPosition - transform.position;
                        _metersToPastTargetPosition = _magnitudaToPastPlayerPosition.magnitude;

                        _searchPath.transform.position = vector3PastPlayerPosition;

                        _target = _searchPath.transform;
                        _agent.destination = _target.position;
                        _searchFind = true;
                    }
                    _searchTime += Time.deltaTime;
                    if (_metersToPastTargetPosition <= 1f | _searchTime > 5f)
                    {
                        _searchTime = 0f;
                        _isSee = false;
                        _search = false;
                        Patroling();
                    }

                }

               // if (_exitDoorScript._open == true)
               // {
               //     _agent.speed = 7f;
               // }
               // else
               // {
               //     _agent.speed = 5f;
               // }
                _agent.destination = _target.position;
                if (_shoutTime > 7f && MetersToPlayer >= 15f)
                {
                    _isSee = false;
                    Patroling();
                }
                if (MetersToPlayer <= 1.2f)
                {
                    Player.Instance.Die();
                }
            }
        }
        else if (MetersToPlayer <= 5f && _playerMovet.Run == true)
        {
            _isSee = true;
            _agent.destination = _target.position;
        }
        if (MetersToPlayer > _viewRadius && _patroling == false)
        {
            _isSee = false;
            Patroling();
        }
        if (MetersToPlayer <= 5f && _playerMovet._crouch == false)
        {
            _isSee = true;
        }
        if (_metersToTarget <= 1f)
        {
            Patroling();
        }
    }
    private void Patroling()
    {
        CanShout = true;
        _searchFind = false;
        if (_isSee == false)
        {
            _viewRadius = 13f;
            _canPlayAudio = true;

           // if (_exitDoorScript._open == true)
           // {
           //     _agent.speed = 6f;
           // }
           // else
           // {
           //     _agent.speed = 4f;
           // }
            _shoutTime = 0f;
            int _randomIndex = Random.Range(0, _patrolingPointArray.Length);
            _target = _patrolingPointArray[_randomIndex];
            _patroling = true;
        }
    }

    private void Disable() => gameObject.SetActive(false);

    private void OnDestroy()
    {
        ExitDoor.OnPlayerWin -= Disable;
    }
}
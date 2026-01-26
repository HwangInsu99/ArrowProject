using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _controller;

    private string _paramAtkSpeed = "fAtkSpeed";
    private string _paramAtk = "tShoot";

    private float _power;
    private float _atkAniSpeed = 1.0f;
    private float _atkRate;
    private float _maxRange;
    private float _servantPower;
    private float _poison;
    private float _moveSpeed = 2.0f;
    private float _maxDistance = 4.0f;
    private Vector3 _startPos;

    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
        if (_controller == null)
            _controller = GetComponent<CharacterController>();

        if (_controller == null || _animator == null)
        {
            print("애니메이터 or 컨트롤로 없음 / 인스펙터 확인");
            return;
        }
    }

    void Start()
    {
        _animator.SetFloat(_paramAtkSpeed, _atkAniSpeed);
        _startPos = transform.position;
    }

    void Update()
    {
        CharacterMove();
        _animator.SetTrigger(_paramAtk);
    }

    void CharacterMove()
    {
        float move = Input.GetAxis("Horizontal");
        Vector3 input = new Vector3(move, 0, 0);
        Vector3 movement = input * _moveSpeed * Time.deltaTime;

        Vector3 nextPos = transform.position + movement;
        Vector3 fromStart = nextPos - _startPos;

        if (fromStart.magnitude > _maxDistance)
        {
            fromStart = fromStart.normalized * _maxDistance;
            nextPos = _startPos + fromStart;

            movement = nextPos - transform.position;
        }

        _controller.Move(movement);
    }
}

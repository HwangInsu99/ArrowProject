using System;
using UnityEngine;

public enum StatType
{
    CriticalPer,
    ArrowPower,
    ArrowSpeed,
    AttackRate,
    PlayerHp,
    ArrowRange,
    MoveSpeed
}
// 아이템 + 펜스는 Power 이후의 6개만 나오므로 파워 이전에 늘어날때마다 item의 _minNum 수정
// 선택지에 안내보낼 것들은 가장 뒤로

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private ArrowSpawner _spawner;

    private string _paramAtkSpeed = "fAtkSpeed";
    private string _paramAtk = "tShoot";

    [SerializeField] private float _power = 1;
    [SerializeField] private float _range = 1;
    [SerializeField] private float _arrowSpeed = 1;
    private float _critPer = 0;
    private bool _isPenetrate = false;

    public float _hp { get; private set; } = 100;
    [SerializeField] private float _atkAniSpeed = 1.5f;
    [SerializeField] private float _atkRate = 1.0f;
    private float _atkCool;
    [SerializeField] private float _moveSpeed = 2.0f;
    private float _maxDistance = 4.0f;
    private Vector3 _startPos;
    public bool _isMaxSpeed { get; private set; } = false;

    public event Action<float> OnDamaged;
    public event Action<Player> OnDead;

    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
        if (_controller == null)
            _controller = GetComponent<CharacterController>();
        if (_spawner == null)
            _spawner = GetComponentInChildren<ArrowSpawner>();

        if (_controller == null || _animator == null)
        {
            Debug.LogError("애니메이터 or 컨트롤로 없음 / 인스펙터 확인");
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
        if (_atkCool <= 0)
            ArrowFire();
        CharacterMove();
        _animator.SetTrigger(_paramAtk);
        _atkCool -= Time.deltaTime;
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

    void ArrowFire()
    {
        _spawner.SpawnArrow(_arrowSpeed, _range, _power, _critPer, _isPenetrate);
        _atkCool = _atkRate;
    }

    public bool PlayerDamaged(float damage)
    {
        _hp -= damage;
        OnDamaged?.Invoke(_hp);

        if ( _hp <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        Debug.Log("게임 종료 로비로");
        OnDead?.Invoke(this);
        GameManager.Instance.PauseGame(true);
    }

    public void ParameterChange(StatType type, int value)
    {
        // 나중에 이동속도 같은거에는 최소 최댓값 Clamp 설정하기
        switch (type)
        {
            case StatType.CriticalPer:
                _critPer += value;
                break;
            case StatType.ArrowPower:
                _power *= Mathf.Pow(1.1f, value);
                break;
            case StatType.AttackRate:
                _atkRate *= Mathf.Pow(0.9f, value);
                _atkAniSpeed *= Mathf.Pow(1.1f, value);
                _animator.SetFloat(_paramAtkSpeed, _atkAniSpeed);
                break;
            case StatType.ArrowSpeed:
                _arrowSpeed *= Mathf.Pow(1.1f, value);
                break;
            case StatType.PlayerHp:
                _hp += value;
                break;
            case StatType.ArrowRange:
                _range *= Mathf.Pow(1.1f, value);
                break;
            case StatType.MoveSpeed:
                _moveSpeed *= Mathf.Pow(1.1f, value);
                if (_moveSpeed >= 8.0f)
                {
                    _moveSpeed = 8.0f;
                    _isMaxSpeed = true;
                }
                break;
        }
    }
}

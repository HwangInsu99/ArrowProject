using System;
using UnityEngine;

public enum EStatType
{
    ArrowPower,
    ArrowSpeed,
    AttackRate,
    ArrowRange,
    PlayerHp,
    SwordCreate,
    SwordPower,
    SwordRange,
    SwordRate,
    SwordSpeed,
    MoveSpeed,
    CriticalPer,
    LifeSteal,
    SpeedDamage,
    SummonPet,
    ArmorUp
}

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private ArrowSpawner _spawner;
    [SerializeField] private SwordManager _swordManager;
    [SerializeField] private Finder _finder;
    [SerializeField] private PetSpawner _petSpawner;

    private string _paramAtkSpeed = "fAtkSpeed";
    private string _paramAtk = "tShoot";

    private float _arrowPower;
    [SerializeField] private float _power = 1;
    [SerializeField] private float _speedPower = 1;
    [SerializeField] private float _range = 1;
    [SerializeField] private float _arrowSpeed = 1;
    [SerializeField] private float _swordSpeed = 1;
    private float _critPer = 0.0f;
    private float _suckPer = 0.0f;
    private float _speedPowerPer = 0.0f;
    [SerializeField] private bool _isPenetrate = false;
    public float _hp { get; private set; } = 100;
    [SerializeField] private float _atkAniSpeed = 1.5f;
    [SerializeField] private float _atkRate = 1.0f;
    [SerializeField] private float _swordPower = 1;
    private float _atkCool;
    [SerializeField] private float _moveSpeed = 2.0f;
    private float _maxDistance = 4.0f;
    private float _armor;
    private float _reduceDamage = 1.0f;
    private Vector3 _startPos;

    public event Action<float> OnHpChanged;
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
        _startPos = transform.position;
        SetParameter();
        _animator.SetFloat(_paramAtkSpeed, _atkAniSpeed);
        CalcPower();
        _atkCool = 0.5f;
    }

    void Update()
    {
        if (_atkCool <= 0)
            ArrowFire();

        CharacterMove();
        _animator.SetTrigger(_paramAtk);
        _atkCool -= Time.deltaTime;
    }

    void SetParameter()
    {
        GameData stat = GameData.Instance;
        ParameterChange(EStatType.PlayerHp, stat.HP);
        ParameterChange(EStatType.ArrowPower, stat.Power);
        ParameterChange(EStatType.AttackRate, stat.Rate);
        ParameterChange(EStatType.SwordCreate, stat.SwordNum);
        ParameterChange(EStatType.SpeedDamage, stat.SpeedD);
        ParameterChange(EStatType.CriticalPer, stat.Crit);
        ParameterChange(EStatType.LifeSteal, stat.LifeSteal);
        ParameterChange(EStatType.ArmorUp, stat.ReduceD);
        _isPenetrate = stat.Penetrate;
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
        _spawner.SpawnArrow(_arrowSpeed, _range, _arrowPower, _critPer, _suckPer, _isPenetrate);
        _atkCool = _atkRate;
    }

    public bool PlayerDamaged(float damage)
    {
        _hp -= damage * _reduceDamage;
        OnHpChanged?.Invoke(_hp);

        if ( _hp <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        Debug.Log("플레이어 사망");
        OnDead?.Invoke(this);
        GameManager.Instance.GameOver();
    }

    public void DataAnalyze(UpgradeDataSO data)
    {
        foreach (var bundle in data.Infos)
        {
            ParameterChange(bundle.type, bundle.value);
        }
    }

    public void ParameterChange(EStatType type, float value)
    {
        switch (type)
        {
            case EStatType.ArrowPower:
                _power *= Mathf.Pow(1.1f, value);
                CalcPower();
                break;
            case EStatType.AttackRate:
                _atkRate *= Mathf.Pow(0.9f, value);
                _atkAniSpeed *= Mathf.Pow(1.1f, value);
                _animator.SetFloat(_paramAtkSpeed, _atkAniSpeed);
                break;
            case EStatType.ArrowSpeed:
                _arrowSpeed *= Mathf.Pow(1.1f, value);
                CalcPower();
                break;
            case EStatType.PlayerHp:
                _hp += value;
                OnHpChanged?.Invoke(_hp);
                break;
            case EStatType.ArrowRange:
                _range *= Mathf.Pow(1.1f, value);
                break;
            case EStatType.SwordCreate:
                _swordManager.CreateSword((int)value, _swordPower, _swordSpeed, _finder._currentTarget);
                break;
            case EStatType.SwordPower:
                _swordPower *= Mathf.Pow(1.1f, value);
                _swordManager.PowerUp(_swordPower);
                break;
            case EStatType.SwordRange:
                _finder.IncreasArea();
                break;
            case EStatType.SwordRate:
                _swordManager.SpawnRate((int)value);
                break;
            case EStatType.SwordSpeed:
                _swordSpeed *= Mathf.Pow(1.1f, value);
                _swordManager.SetSpeed(_swordSpeed);
                break;
            case EStatType.MoveSpeed:
                _moveSpeed *= Mathf.Pow(1.1f, value);
                if (_moveSpeed >= 8.0f)
                {
                    _moveSpeed = 8.0f;
                }
                break;
            case EStatType.CriticalPer:
                _critPer += value;
                break;
            case EStatType.LifeSteal:
                _suckPer += value;
                break;
            case EStatType.SpeedDamage:
                _speedPowerPer += value;
                _speedPower = _arrowSpeed * (1.0f + _speedPowerPer * 0.01f);
                
                CalcPower();
                break;
            case EStatType.SummonPet:
                _petSpawner.SpawnPet((int)value, transform);
                break;
            case EStatType.ArmorUp:
                _armor += value;
                _reduceDamage -= _armor * 0.01f;
                _reduceDamage = Mathf.Clamp(_reduceDamage, 0.5f, 1.0f);
                break;
        }
    }

    void CalcPower() => _arrowPower = _power * _speedPower;
}

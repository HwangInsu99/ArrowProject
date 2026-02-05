using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private enum State
    {
        Rest,
        Ready,
        Chase
    }
    [SerializeField] private Transform _target;
    [SerializeField] private SwordManager _myManager;

    private float _baseDamage = 15.0f;
    [SerializeField] private float _damage;
    private float _speed = 5.0f;
    private float _spreadValue = 0.5f;
    private float _coolTimer;
    private State _state;

    void Awake()
    {
        _myManager = GetComponentInParent<SwordManager>();
    }

    void Start()
    {
        Init();
    }

    void OnEnable()
    {
        Init();
    }

    void Update()
    {
        if (_target == null)
            return;

        if (_state != State.Chase)
            return;
        
        SwordMove();
    }

    void Init()
    {
        _state = State.Ready;

        Vector2 pos = Random.insideUnitCircle * _spreadValue;
        transform.localPosition += new Vector3(pos.x, pos.y, 0.0f);
        _coolTimer = 0.0f;
        if (_target != null)
        {
            _state = State.Chase;
            transform.SetParent(null);
        }
    }

    void SwordMove()
    {
        if (_target == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public void SetDamage(float damage)
    {
        _damage = _baseDamage * damage;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        if(_state == State.Ready)
        {
            _state = State.Chase;
            transform.SetParent(null);
        }
    }

    public void ClearTarget()
    {
        Debug.Log("Å¸°Ù »ç¶óÁü");
        _target = null;
        bool isRest = (_state == State.Rest);

        _state = State.Rest;
        _myManager.ReturnPool(gameObject, isRest);
    }

    public float Damage() => _damage;

    public void OnHit()
    {
        _state = State.Rest;
        _myManager.ReturnPool(gameObject, true);
    }

    public void CoolTimer(float timer)
    {
        if (_state != State.Rest)
            return;

        _coolTimer += Time.deltaTime;

        if (_coolTimer >= timer)
            gameObject.SetActive(true);
    }
}

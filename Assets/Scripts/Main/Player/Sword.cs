using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private SwordManager _myManager;

    private float _baseDamage = 15.0f;
    private float _damage;
    private float _speed;
    private float _spreadValue = 0.5f;
    private bool _isFire = false;

    void Awake()
    {
        _myManager = GetComponentInParent<SwordManager>();
    }

    private void OnEnable()
    {
        Vector2 pos = Random.insideUnitCircle * _spreadValue;
        transform.localPosition = new Vector3(pos.x, pos.y, 0.0f);
    }

    void Update()
    {
        if (_target == null)
        {
            if (_isFire)
            {
                _isFire = false;
                _myManager.ReturnPool(gameObject, false);
            }
            return;
        }
            
        if (!_isFire)
            _isFire = true;
        
        SwordMove();
    }

    void SwordMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public void SetDamage(float damage)
    {
        _damage = _baseDamage * damage;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public float Damage()
    {
        return _damage;
    }

    public void OnHit()
    {
        //스포너한테 전달
        _myManager.ReturnPool(gameObject, true);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _firePrefab;
    [SerializeField] private float _damage;
    private Vector3 _offset;
    private float _moveSpeed = 2.0f;
    private float _fireRate = 3.0f;
    private float _coolTime;
    private int _startPool = 3;

    private readonly Queue<GameObject> _pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < _startPool; i++)
        {
            AddPool();
        }
        float xPos = Random.Range(-3.0f, 3.0f);
        _offset = new Vector3(xPos, 1, 0);
        transform.position += _offset;
        _coolTime = 1.0f;
    }

    void Update()
    {
        if (_firePrefab == null)
            return;

        _coolTime -= Time.deltaTime;
        if (_coolTime <= 0.0f)
            Attack();
    }

    void LateUpdate()
    {
        if (_player == null)
            return;

        Vector3 targetPos = _player.position + _offset;
        targetPos.x = Mathf.Clamp(targetPos.x, -4.0f, 4.0f);

        transform.position = Vector3.Lerp
            (
                transform.position,
                targetPos,
                _moveSpeed * Time.deltaTime
            );
    }

    void AddPool()
    {
        GameObject fire = Instantiate(_firePrefab, transform);
        fire.SetActive(false);
        _pool.Enqueue(fire);
    }

    public void SetParam(float damage, Transform target, GameObject prefab)
    {
        _damage = damage;
        _player = target;
        _firePrefab = prefab;
    }

    void Attack()
    {
        if (_pool.Count <= 0)
        {
            AddPool();
        }
        _coolTime = _fireRate;
        GameObject fire = _pool.Dequeue();
        PetFire scFire = fire.GetComponent<PetFire>();
        scFire.SetDamage(_damage);
        fire.transform.SetParent(null);
        fire.SetActive(true);
    }

    public void DespawnFire(GameObject fire)
    {
        fire.transform.SetParent(transform);
        fire.transform.localPosition = Vector3.zero;
        fire.SetActive(false);
        _pool.Enqueue(fire);
    }
}

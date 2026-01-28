using UnityEngine;

public class Arrow : MonoBehaviour, ArrowSpawner.ISpawnListener
{
    [SerializeField] private ArrowSpawner _mySpawner;

    private float _baseSpeed = 3.0f;
    private float _baseDistance = 5.0f;
    private float _baseDamage = 10.0f;

    private float _speed;
    private float _maxDistance;
    private float _remainDistance;
    private float _damage;
    private float _critPer = 0.0f;

    bool _isPenetrate = false;

    void Awake()
    {
        _mySpawner = GetComponentInParent<ArrowSpawner>();
    }

    void Update()
    {
        ArrowMove();
        if (_remainDistance <= 0.0f)
            _mySpawner.DespawnArrow(gameObject);
    }

    public void SetStatus(float speed, float distance, float damage, float crit, bool penetrate, Transform point)
    {
        _speed = _baseSpeed * speed;
        _maxDistance = _baseDistance * distance;
        _damage = _baseDamage * damage;
        _critPer = crit;
        _isPenetrate = penetrate;
        transform.position = point.position;
        _remainDistance = _maxDistance;
    }

    void ArrowMove()
    {
        float dist = _speed * Time.deltaTime;
        _remainDistance -= dist;
        transform.position += transform.forward * dist;
    }

    bool CriticalAttack(float critPer)
    {
        float criticalRate = critPer * 0.01f;        
        return Random.value < criticalRate;
    }

    // 2개보다 전달할 데이터가 많아지면 구조체로 승격 구조체는 모노비헤이비어를 안쓰는 스크립트에서 정의
    public (float damage, bool isCrit ) Damage()
    {
        bool isCrit = CriticalAttack(_critPer);
        float arrowDamage = isCrit ? _damage * 2 : _damage;
        return (arrowDamage, isCrit);
    }

    public void OnHit()
    {
        if (!_isPenetrate)
        {
            _mySpawner.DespawnArrow(gameObject);
        }
    }
}

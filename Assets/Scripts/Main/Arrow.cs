using UnityEngine;

public class Arrow : MonoBehaviour, ArrowSpawner.ISpawnListener
{
    [SerializeField] private ArrowSpawner _mySpawner;
    [SerializeField] private string _targetLayer = "Enemy";

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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(_targetLayer))
        {
            bool isCrit = CriticalAttack(_critPer);
            float arrowDamage = isCrit ? _damage * 2 : _damage;
            // 적에게 _damage를 전달하는 코드
            if (_isPenetrate == false)
                _mySpawner.DespawnArrow(gameObject);
        }
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
}

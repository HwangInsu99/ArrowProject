using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private FenceSpawner _fenceSpawner;
    [SerializeField] private UIManager _uiManager;
    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemy;
    }
    public EnemyType[] _enemyType;

    [SerializeField] private float _setDist = 3.5f;
    // 능력치 강화 팬스가 나오고 몇 초 후에 만들지
    private float _maxTime;
    private float _minTime;
    private float _spawnTimer;
    private int _spawnBossCount = 2;
    private int _nextBossCount = 6;
    private float _standardHp = 50;
    private int _section;

    void Start()
    {
        if (_enemyType == null)
        {
            Debug.LogWarning("소환할 적이 없음");
            return;
        }

        SpawnEnemy(_enemyType[0]);
        GameManager.Instance.ChangeEnemyBaseHP(_standardHp);
    }

    void Update()
    {
        if (_enemyType == null)
        {
            return;
        }

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            if (_spawnBossCount == 0)
            {
                SpawnEnemy(_enemyType[1]);
                _spawnBossCount = _nextBossCount;
                SectionChange();
            }
            else
            {
                SpawnEnemy(_enemyType[0]);
                _spawnBossCount--;
            }
        }
    }

    void SpawnEnemy(EnemyType enemy)
    {
        Vector3 spawnPos = transform.position;
        if (_spawnBossCount != 0)
        {
            float dir = Random.value < 0.5f ? 1f : -1f;
            spawnPos += transform.right * _setDist * dir;
        }            

        GameObject go = Instantiate(enemy.enemy, spawnPos, Quaternion.Euler(0, 180, 0));
        go.GetComponent<Enemy>().SetHp(_standardHp, _section);
        GameManager.Instance.CallHpUI(go);
        SetSpawnTimer();
    }

    void SetSpawnTimer()
    {
        if (_fenceSpawner == null)
            return;

        float time = _fenceSpawner.GetTimer();

        if (_spawnBossCount == 1)
        {
            // 보스가 소환되는 경우
            _spawnTimer = time - 1.0f;
            return;
        }
        
        _minTime = time + 0.5f;
        _maxTime = time + 4.0f;
        _spawnTimer = Random.Range(_minTime, _maxTime);
    }

    void SectionChange()
    {
        _section++;
        _standardHp += _section * 100.0f;
        GameManager.Instance.ChangeEnemyBaseHP(_standardHp);
    }
}

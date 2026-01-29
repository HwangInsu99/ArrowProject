using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private FenceSpawner _fenceSpawner;
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

    void Start()
    {
        if (_enemyType == null)
        {
            Debug.LogWarning("소환할 적이 없음");
            return;
        }

        SpawnEnemy(_enemyType[0]);
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
            }
            else
            {
                SpawnEnemy(_enemyType[0]);
                _spawnBossCount--;
            }
        }
        // 나중에 디테일 작업 때 소환 빈도 언제 보스 소환 할지 등등
    }
    // 팬스 스포너의 다음 소환까지의 시간을 확인 에네미 스포너는 팬스를 기준으로 2초 전 / 후의 생성 시간 범위를 갖는다.
    // Ex) 스포너의 남은 시간 +2.0  ~ 스포너의 남은 시간 + 소환주기 -4.0
    // 보스는 스포너의 남은시간 - 2.0
    void SpawnEnemy(EnemyType enemy)
    {
        Vector3 spawnPos = transform.position;
        if (_spawnBossCount != 0)
        {
            float dir = Random.value < 0.5f ? 1f : -1f;
            spawnPos += transform.right * _setDist * dir;
        }            

        Instantiate(enemy.enemy, spawnPos, Quaternion.Euler(0, 180, 0));
        SetSpawnTimer();
    }

    void SetSpawnTimer()
    {
        if (_fenceSpawner == null)
            return;

        float time = _fenceSpawner.GetTimer();

        if (_spawnBossCount == 0)
        {
            // 보스가 소환되는 경우
            _spawnTimer = time - 1.0f;
            return;
        }
        
        _minTime = time + 0.5f;
        _maxTime = time + 4.0f;
        _spawnTimer = Random.Range(_minTime, _maxTime);
    }
}

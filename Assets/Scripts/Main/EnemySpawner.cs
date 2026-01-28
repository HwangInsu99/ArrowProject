using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
        // 나중에 디테일 작업 때 소환 빈도 언제 보스 소환 할지 등등
    }

    void SpawnEnemy(EnemyType enemy)
    {
        float dir = Random.value < 0.5f ? 1f : -1f;
        Vector3 spawnPos = transform.position + transform.right * _setDist * dir;            

        Instantiate(enemy.enemy, spawnPos, Quaternion.Euler(0, 180, 0));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _fence;
    private float _spawnRate = 6.0f;
    private float _spawnTimer = 4.0f;
    private float _spawnDist = 3.0f;
    void Start()
    {
        if (_fence == null)
        {
            Debug.LogWarning("펜스 없음 인스턴스 확인");
            return;
        }
    }

    void Update()
    {
        if ( _fence == null )
            return;

        _spawnTimer -= Time.deltaTime;

        if ( _spawnTimer <= 0)
        {
            SpawnFence();
        }
    }

    void SpawnFence()
    {
        Vector3 spawnPos1 = transform.position + transform.right * _spawnDist;
        Vector3 spawnPos2 = transform.position + transform.right * -_spawnDist;        

        GameObject fence1 = Instantiate(_fence, spawnPos1, Quaternion.identity);
        GameObject fence2 = Instantiate(_fence, spawnPos2, Quaternion.identity);

        GameManager.Instance.CallItemUI(fence1);
        GameManager.Instance.CallItemUI(fence2);

        _spawnTimer = _spawnRate;
    }

    public float GetTimer() => _spawnTimer;
}

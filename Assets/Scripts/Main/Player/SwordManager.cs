using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [SerializeField] private GameObject _sword;

    private readonly List<Sword> _swords = new List<Sword>();

    private Transform _spawnPoint;
    private float _baseCool = 5.0f;
    [SerializeField] private float _coolTime;
    private int _decreaseSpawnRate = 100;

    void Start()
    {
        if (_sword == null)
        {
            Debug.LogWarning("Sword매니저에 검 오브젝트 없음");
            return;
        }
        _spawnPoint = transform;
        _coolTime = _baseCool;
    }

    void Update()
    {
        if (_swords.Count == 0) return;
        foreach (Sword sword in _swords)
        {
            sword.CoolTimer(_coolTime);
        }
    }

    public void CreateSword(int num, float power, float speed, Transform target)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject sword = Instantiate(_sword, _spawnPoint);
            if (Random.value < 0.5f)
                sword.transform.localPosition = Vector3.right;
            else
                sword.transform.localPosition = Vector3.left;
            Sword scSword = sword.GetComponent<Sword>();
            scSword.SetDamage(power);
            scSword.SetSpeed(speed);
            _swords.Add(scSword);
            if (target != null)
                scSword.SetTarget(target);
        }
    }

    public void PowerUp(float power)
    {
        if (_swords.Count == 0) return;
        foreach (Sword sword in _swords)
            sword.SetDamage(power);
    }

    public void SetSpeed(float speed)
    {
        if (_swords.Count == 0) return;
        foreach (Sword sword in _swords)
            sword.SetSpeed(speed);
    }

    public void SendTarget(Transform target)
    {
        if (_swords.Count == 0) return;

        if (target == null)
        {
            TargetLost();
            return;
        }

        foreach (Sword sword in _swords)
        {
            sword.SetTarget(target);
        }
    }

    public void TargetLost()
    {
        if (_swords.Count == 0)
            return;
        Debug.Log("타겟 소실");
        foreach (Sword sword in _swords)
            sword.ClearTarget();
    }

    public void SpawnRate(int time)
    {
        if (_coolTime <= 0)
        {
            _coolTime = 0;
            return;
        }
            
        _decreaseSpawnRate -= time;
        float x = _decreaseSpawnRate * 0.01f;
        _coolTime = _baseCool * x;
    }

    public void ReturnPool(GameObject sword, bool hit)
    {
        sword.SetActive(false);

        sword.transform.SetParent(_spawnPoint, false);

        sword.transform.localPosition = Vector3.zero;

        if (Random.value < 0.5f)
            sword.transform.localPosition = Vector3.right;
        else
            sword.transform.localPosition = Vector3.left;

        if (!hit)
        {
            sword.SetActive(true);
        }
    }
}

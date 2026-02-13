using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ArrowType
    {
        public GameObject arrow;
    }
    public ArrowType[] _arrowType;

    private readonly Queue<GameObject> _pool = new Queue<GameObject>();
    private Transform _firePoint;

    void Start()
    {
        if (_arrowType == null)
        {
            Debug.LogWarning("화살 안넣었음");
            return;
        }
        _firePoint = transform;
        for (int i = 0; i < 100; i++)
        {
            AddPool();
        }
    }

    void AddPool()
    {
        GameObject arrow = Instantiate(_arrowType[0].arrow, _firePoint);
        arrow.SetActive(false);
        _pool.Enqueue(arrow);
    }

    public void SpawnArrow(float speed, float distance, float damage, float crit, float suck, bool penetrate)
    {
        if (_pool.Count <= 0)
        {
            AddPool();
        }
        GameObject arrow = _pool.Dequeue();
        Arrow script = arrow.GetComponent<Arrow>();
        if (script != null)
        {
            script.SetStatus(speed, distance, damage, crit, suck, penetrate, transform);
        }
        arrow.transform.SetParent(null);
        arrow.SetActive(true);
        SoundManager.Instance.PlaySfx(ESfxType.Arrow);
    }

    public void DespawnArrow(GameObject arrow)
    {
        //Debug.Log("회수");
        arrow.transform.SetParent(_firePoint);
        arrow.SetActive(false);
        _pool.Enqueue(arrow);
    }
}

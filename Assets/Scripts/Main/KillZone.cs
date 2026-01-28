using Unity.VisualScripting;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private ArrowSpawner _spawner;
    [SerializeField] private string _arrowLayer = "Arrow";
    [SerializeField] private string _enemyLayer = "Enemy";

    private void Start()
    {
        if (_spawner == null)
            Debug.LogWarning("이 킬존에 스포너 참조 안했음", this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer(_arrowLayer))
        {
            Debug.Log("화살 닿았음", this);
            _spawner.DespawnArrow(other.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(_enemyLayer))
        {
            Debug.Log("적 닿았음", this);
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.KillZone();
        }
    }
}

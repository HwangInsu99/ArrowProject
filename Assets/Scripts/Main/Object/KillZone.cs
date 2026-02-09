using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private ArrowSpawner _spawner;
    [SerializeField] private string _attackLayer = "Attack";
    [SerializeField] private string _enemyLayer = "Enemy";
    [SerializeField] private string _itemLayer = "Item";

    private void Start()
    {
        if (_spawner == null)
            Debug.LogWarning("이 킬존에 스포너 참조 안했음", this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer(_attackLayer))
        {
            Debug.Log("투사체 닿았음", this);
            if (other.gameObject.CompareTag("Arrow"))
            {
                _spawner.DespawnArrow(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Fire"))
            {
                other.GetComponent<PetFire>().Despawn();
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(_enemyLayer))
        {
            Debug.Log("적 닿았음", this);
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Break();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(_itemLayer))
        {
            Debug.Log("아이템 닿았음", this);
            Item item = other.GetComponent<Item>();
            item.Break();
        }
    }
}

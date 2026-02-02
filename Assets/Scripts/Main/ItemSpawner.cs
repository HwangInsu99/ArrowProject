using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    void Start()
    {
        if ( _item == null )
        {
            Debug.LogWarning("아이템 없음");
            return;
        }
    }
    
    public void SpawnItem()
    {
        if (_item == null) return;
        Instantiate(_item, transform.position, Quaternion.identity);
    }
}

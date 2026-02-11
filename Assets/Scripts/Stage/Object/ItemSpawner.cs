using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    [SerializeField] private Player _player;
    void Start()
    {
        if ( _item == null )
        {
            Debug.LogWarning("아이템 없음");
            return;
        }
    }
    
    public void SpawnItem(Vector3 pos)
    {
        if (_item == null) return;

        GameObject item = Instantiate(_item, pos, Quaternion.identity);
        GameManager.Instance.CallItemUI(item);
    }
}

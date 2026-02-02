using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string _playerLayer = "Player";

    private float _speed = 2.0f;
    private StatType _type;
    private int _value;

    void Start()
    {
        SetStatus();
    }

    void Update()
    {
        ItemMove();
    }

    void SetStatus()
    {
        var enumvalue = System.Enum.GetValues(enumType: typeof(StatType));

        _type = (StatType)enumvalue.GetValue(Random.Range(0, enumvalue.Length - 1));        
        _value = Random.Range(1, 6);
    }

    void ItemMove()
    {
        transform.position -= transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
        {
            Player player = other.GetComponent<Player>();
            player.ParameterChange(_type, _value);
            Destroy(gameObject);
        }
    }

    public void KillZone()
    {
        Destroy(gameObject);
    }
}

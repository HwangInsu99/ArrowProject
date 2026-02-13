using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string _playerLayer = "Player";

    private float _speed = 2.0f;
    private EStatType _type;
    private float _value;
    private int _maxNum;
    public bool _isMaxSpeed;

    public event Action<Item> OnBreaked;

    void Awake()
    {
        SetStatus();
    }

    void Update()
    {
        ItemMove();
    }

    void SetStatus()
    {
        var enumvalue = Enum.GetValues(enumType: typeof(EStatType));
        _maxNum = enumvalue.Length - 5;

        if (_isMaxSpeed) _maxNum--;

        _type = (EStatType)enumvalue.GetValue(UnityEngine.Random.Range(0, _maxNum));
        _value = RandomRank();
        if (_type == EStatType.PlayerHp)
            HpValueSet(_value);
    }

    void ItemMove()
    {
        transform.position -= transform.forward * _speed * Time.deltaTime;
    }

    int RandomRank()
    {
        float rank4 = 0.01f;
        float rank3 = 0.05f;
        float rank2 = 0.14f;
        //float rank1 = 0.8f;

        float rand = UnityEngine.Random.value;

        if (rand < rank4)
            return 4;
        else if (rand < rank4 + rank3)
            return 3;
        else if (rand < rank4 + rank3 + rank2)
            return 2;
        else
            return 1;
    }

    void HpValueSet(float value)
    {
        float min = 0;
        float max = 0;        
        switch (value)
        {
            case 1:
                min = 0.5f;
                max = 1.0f;
                break;
            case 2:
                min = 1.1f;
                max = 1.5f;
                break;
            case 3:
                min = 1.8f;
                max = 2.2f;
                break;
            case 4:
                min = 2.8f;
                max = 3.5f;
                break;
        }
        float result = UnityEngine.Random.Range(min, max);
        _value = GameManager.Instance._enemyBaseHp * result;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
        {
            Player player = other.GetComponent<Player>();
            player.ParameterChange(_type, _value);
            Break();
        }
    }

    public void Break()
    {
        OnBreaked?.Invoke(this);
        Destroy(gameObject);
    }

    public string Explain()
    {
        string type = "";
        switch (_type)
        {
            case EStatType.ArrowPower:
                type = "화살 공격력";
                break;
            case EStatType.ArrowSpeed:
                type = "화살 속도";
                break;
            case EStatType.AttackRate:
                type = "화살 발사빈도";
                break;
            case EStatType.PlayerHp:
                type = "체력";
                break;
            case EStatType.ArrowRange:
                type = "화살 사거리";
                break;
            case EStatType.MoveSpeed:
                type = "가로 이동속도";
                break;
            case EStatType.SwordCreate:
                type = "검 갯수";
                break;
            case EStatType.SwordPower:
                type = "검 공격력";
                break;
            case EStatType.SwordRange:
                type = "검 사거리";
                break;
            case EStatType.SwordRate:
                type = "검 쿨타임감소";
                break;
            case EStatType.SwordSpeed:
                type = "검 속도";
                break;
        }
        return type + " + " + Mathf.FloorToInt(_value);
    }
}

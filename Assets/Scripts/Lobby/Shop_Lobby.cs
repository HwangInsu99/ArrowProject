using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shop_Title : MonoBehaviour
{
    public enum ShopType
    {
        HP,
        Power,
        Rate,
        Num,
        SpeedDamage,
        Penetrate,
        LifeSteal,
        Crit,
        ReduceDamage
    }

    [SerializeField] private TextMeshProUGUI _money;
    [System.Serializable]
    public class ButtonObject
    {
        public GameObject button;
        public TextMeshProUGUI priceText;
        public int price;
        public ShopType type;
        public int max = 1;
        public int current;
        public TextMeshProUGUI countText;
    }
    [SerializeField] private ButtonObject[] _button = new ButtonObject[9];
    [SerializeField] private int _speedD = 10;
    [SerializeField] private int _lifeSteal = 3;
    [SerializeField] private int _crit = 6;
    [SerializeField] private int _reduceD = 10;

    private void Awake()
    {
        for (int i = 0; i < _button.Length; i++)
        {
            _button[i].priceText = _button[i].button.GetComponentInChildren<TextMeshProUGUI>();
            _button[i].type = (ShopType)i;

            int index = i;
            Button button = _button[i].button.GetComponent<Button>();
            button.onClick.AddListener(() => BuyItem(index));

            if (_button[i].countText != null)
                _button[i].max = 3;

            if (_button[i].priceText == null)
                continue;
            _button[i].price = SetPrice(_button[i].type);
            _button[i].priceText.text = _button[i].price.ToString();
        }
    }

    private void OnEnable()
    {
        SetMoney();
        SetItem();
    }

    int SetPrice(ShopType type)
    {
        int price = 0;

        switch (type)
        {
            case ShopType.HP:
                price = 10;
                break;
            case ShopType.Power:
                price = 10;
                break;
            case ShopType.Rate:
                price = 10;
                break;
            case ShopType.Num:
                price = 10;
                break;
            case ShopType.SpeedDamage:
                price = 20;
                break;
            case ShopType.Penetrate:
                price = 30;
                break;
            case ShopType.LifeSteal:
                price = 20;
                break;
            case ShopType.Crit:
                price = 40;
                break;
            case ShopType.ReduceDamage:
                price = 30;
                break;
        }
        return price;
    }

    void BuyItem(int index)
    {
        var item = _button[index];
        Debug.Log($"{index + 1}¹øÂ° ¹öÆ°");

        if (GameData.Instance.Money < item.price)
            return;

        item.current++;

        if (item.countText != null)
            item.countText.text = $"{item.current} / {item.max}";
        switch (item.type)
        {
            case ShopType.HP:                
                GameData.Instance.UpgradeHP();
                break;
            case ShopType.Power:
                GameData.Instance.UpgradePower();
                break;
            case ShopType.Rate:
                GameData.Instance.UpgradeRate();
                break;
            case ShopType.Num:
                GameData.Instance.UpgradeSwordNum();
                break;
            case ShopType.SpeedDamage:
                GameData.Instance.UpgradeSpeedD(_speedD);
                break;
            case ShopType.Penetrate:
                GameData.Instance.UpgradePenetrate();
                break;
            case ShopType.LifeSteal:
                GameData.Instance.UpgradeLifeSteal(_lifeSteal);
                break;
            case ShopType.Crit:
                GameData.Instance.UpgradeCrit(_crit);
                break;
            case ShopType.ReduceDamage:
                GameData.Instance.UpgradeReduceD(_reduceD);
                break;
        }

        if(item.current == item.max)
            item.button.SetActive(false);

        GameData.Instance.ChangeMoney(-item.price);
        SetMoney();
    }

    void SetMoney()
    {
        _money.text = "µ·: " + GameData.Instance.Money;
    }

    void SetItem()
    {        
        for (int i = 0; i < _button.Length; i++)
        {
            var item = _button[i];

            item.current = TypeData(item.type);

            if (item.countText != null)
                item.countText.text = $"{item.current} / {item.max}";

            if (item.current == item.max)
                item.button.SetActive(false);
        }
    }

    int TypeData(ShopType type)
    {
        return type switch
        {
            ShopType.HP => GameData.Instance.HP,
            ShopType.Power => GameData.Instance.Power,
            ShopType.Rate => GameData.Instance.Rate,
            ShopType.Num => GameData.Instance.SwordNum,
            ShopType.SpeedDamage => GameData.Instance.SpeedD > 0 ? 1 : 0,
            ShopType.Penetrate => GameData.Instance.Penetrate ? 1 : 0,
            ShopType.LifeSteal => GameData.Instance.LifeSteal > 0 ? 1 : 0,
            ShopType.Crit => GameData.Instance.Crit > 0 ? 1 : 0,
            ShopType.ReduceDamage => GameData.Instance.ReduceD > 0 ? 1 : 0,
            _ => 0
        };
    }

}

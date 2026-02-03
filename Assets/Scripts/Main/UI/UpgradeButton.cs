using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _text;

    private StatType _type;
    private int _value;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AddParameter);
    }

    public void SetParameterValue(StatType stat, int rank, int value)
    {
        _type = stat;
        _value = _type == StatType.PlayerHp ? value * 50 : value;
        string sign = _value > 0 ? "+ " : "";
        string explain = TypeTranslation(_type);

        _text.text = $"Grade : {rank}\n" +
            $"{explain} {sign}{_value.ToString()}";
    }

    void AddParameter()
    {
        _player.ParameterChange(_type, _value);
        GameManager.Instance.CompleteUpgrade();
    }

    string TypeTranslation(StatType type)
    {
        switch (type)
        {
            case StatType.CriticalPer:
                return "크리티컬 확률";
            case StatType.ArrowPower:
                return "화살 공격력";
            case StatType.AttackRate:
                return "화살 발사빈도";
            case StatType.ArrowSpeed:
                return "화살 속도";
            case StatType.PlayerHp:
                return "체력";
            default: // 나오면 안되는 값
                return "";
        }
    }
}

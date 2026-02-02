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
        _value = value;
        string sign = _value > 0 ? "+ " : "";
        _text.text = $"Grade : {rank}\n" +
            $"{_type.ToString()} {sign}{_value.ToString()}";
    }

    void AddParameter()
    {
        _player.ParameterChange(_type, _value);
        GameManager.Instance.CompleteUpgrade();
    }
}

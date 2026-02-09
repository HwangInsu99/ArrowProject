using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _text;

    private UpgradeDataSO _data;
    private float _hpValue;
    private bool _hasHpType;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AddParameter);
    }

    public void SetParameterValue(UpgradeDataSO dataBundle)
    {
        _hasHpType = false;
        _data = dataBundle;
        if(_data.Infos.Count == 1 && _data.Infos[0].type == StatType.PlayerHp)
        {
            _hpValue = _data.HpValue();
            _text.text = $"Grade : {_data.Rank}\n{_data.Explain}{_hpValue}";
            _hasHpType = true;
            return;
        }
        _text.text = $"Grade : {_data.Rank}\n{_data.Explain}";
    }

    void AddParameter()
    {
        if (_data == null)
            return;
        if (_hasHpType)
        {
            _player.ParameterChange(StatType.PlayerHp, _hpValue);
        }
        else
            _player.DataAnalyze(_data);
        GameManager.Instance.CompleteUpgrade();
    }
}

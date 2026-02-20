using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;

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
        _image.sprite = _data.SpriteImage;
        if (_data.Infos.Count == 1 && _data.Infos[0].type == EStatType.PlayerHp)
        {
            _hpValue = Mathf.FloorToInt(_data.HpValue());
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
            _player.ParameterChange(EStatType.PlayerHp, _hpValue);
        }
        else
            _player.DataAnalyze(_data);
        GameManager.Instance.CompleteUpgrade();
    }
}

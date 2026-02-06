using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _text;

    private UpgradeDataSO _data;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AddParameter);
    }

    public void SetParameterValue(UpgradeDataSO dataBundle)
    {
        _data = dataBundle;
        _text.text = $"Grade : {_data.Rank}\n{_data.Explain}";
    }

    void AddParameter()
    {
        if (_data == null)
            return;

        _player.DataAnalyze(_data);
        GameManager.Instance.CompleteUpgrade();
    }
}

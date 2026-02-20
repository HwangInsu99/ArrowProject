using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private UpgradeData _upgradeData;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _hpUI;
    [SerializeField] private GameObject _itemUI;
    [SerializeField] private GameObject _endUI;
    [SerializeField] private ScoreUI _scoreUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CallPause();
        }
    }

    public void CallUpgradeData()
    {
        _upgradeData.RandomStat();
        _upgradePanel.transform.SetAsLastSibling();
        _upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        Debug.Log("패널 닫기");
        _upgradePanel.SetActive(false);
    }

    public void CreateHpUI(GameObject target)
    {
        GameObject hpUI = Instantiate(_hpUI, _mainCanvas.transform);
        hpUI.transform.SetAsFirstSibling();
        HpUI ui = hpUI.GetComponent<HpUI>();
        ui.SetHpUI(target.transform, target.GetComponent<Enemy>());
    }

    public void CreateItemUI(GameObject target)
    {
        GameObject itemUI = Instantiate(_itemUI, _mainCanvas.transform);
        itemUI.transform.SetAsFirstSibling();
        ItemUI ui = itemUI.GetComponent<ItemUI>();
        ui.SetItemUI(target.transform, target.GetComponent<Item>());
    }

    public void CallScore(int score) => _scoreUI.ChangeScore(score);
    public void CallMoney() => _scoreUI.SetMoney();
    public void CallPause()
    {
        _pausePanel.SetActive(true);
        GameManager.Instance.PauseGame(true);
    }

    public void ClosePause()
    {
        _pausePanel.SetActive(false);
        GameManager.Instance.PauseGame(false);
    }

    public void CallOption()
    {
        _optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        _optionPanel.SetActive(false);
    }
    public void ReturnTitle()
    {
        GameManager.Instance.ReturnTitle();
    }
    
    public void CallEndUI()
    {
        _endUI.SetActive(true);
    }

    public void CloseEndUI()
    {
        _endUI.SetActive(false);
    }

    public void Restart()
    {
        GameManager.Instance.Restrat();
    }
}

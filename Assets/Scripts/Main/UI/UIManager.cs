using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private UpgradeData _upgradeData;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _hpUI;
    [SerializeField] private GameObject _itemUI;
    [SerializeField] private ScoreUI _scoreUI;


    public void CallUpgradeData()
    {
        _upgradeData.RandomStat();
        _upgradePanel.transform.SetAsLastSibling();
        _upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        Debug.Log("ÆÐ³Î ´Ý±â");
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
        ItemUI ui = itemUI.GetComponent<ItemUI>();
        ui.SetItemUI(target.transform, target.GetComponent<Item>());
    }

    public void CallScore(int score)
    {
        _scoreUI.ChangeScore(score);
    }

    public void CallMoney()
    {
        _scoreUI.SetMoney();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private UpgradeData _upgradeData;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _hpUI;


    public void CallUpgradeData()
    {
        _upgradeData.RandomStat();
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
        HpUI ui = hpUI.GetComponent<HpUI>();
        ui.SetHpUI(target.transform, target.GetComponent<Enemy>());
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ItemSpawner _itemSpawner;
    void Awake()
    {
        Instance = this;
    }

    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    public void CallUpgradeUI()
    {
        _uiManager.CallUpgradeData();
    }

    public void CompleteUpgrade()
    {
        Debug.Log("선택 완료");
        _uiManager.CloseUpgradePanel();
        PauseGame(false);
    }

    public void CallItem(Vector3 pos)
    {
        _itemSpawner.SpawnItem(pos);
    }

    public void CallHpUI(GameObject go)
    {
        _uiManager.CreateHpUI(go);
    }

    public void CallItemUI(GameObject go)
    {
        _uiManager.CreateItemUI(go);
    }

    public void IncreaseScore(int score)
    {
        _uiManager.CallScore(score);
    }

    public void IncreaseMoney(int money)
    {
        GameData.Instance.IncreaseMoney(money);
        _uiManager.CallMoney();
    }
}
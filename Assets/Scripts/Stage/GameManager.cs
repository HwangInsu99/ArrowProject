using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    public static GameManager Instance { get; private set; }
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private Player _player;
    public float _enemyBaseHp { get; private set; }
    void Awake()
    {
        Instance = this;
        if (_uiManager == null) Debug.LogError($"{name}: UIManager 연결 안함");
        if (_itemSpawner == null) Debug.LogError($"{name}: ItemSpawner 연결 안함");
        if (_player == null) Debug.LogError($"{name}: Player 연결 안함");
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

    public void LifeSteal(float suck)
    {
        _player.ParameterChange(StatType.PlayerHp, suck);
    }

    public void ChangeEnemyBaseHP(float hp) => _enemyBaseHp = hp;
    
    public void ReturnTitle()
    {
        PauseGame(false);
        SceneManager.LoadScene("Lobby");
    }
}
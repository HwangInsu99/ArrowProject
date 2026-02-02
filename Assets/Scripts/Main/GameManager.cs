using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] UIManager _uiManager;
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
}

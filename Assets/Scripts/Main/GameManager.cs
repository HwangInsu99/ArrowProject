using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    public void CallUpgradeData()
    {

    }
}

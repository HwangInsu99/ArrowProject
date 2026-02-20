using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIManager _uimanager;
    public void PauseGame()
    {
        _uimanager.CallPause();
    }
    public void ContinueGame()
    {
        _uimanager.ClosePause();
    }

    public void OptionUI()
    {
        _uimanager.CallOption();
    }

    public void CloseOption()
    {
        _uimanager.CloseOption();
    }

    public void ReturnTitle()
    {
        _uimanager.ReturnTitle();
    }

    public void Restart()
    {
        _uimanager.Restart();
    }
}

using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _shopPanel;

    public void GameStart()
    {
        SceneFlowManager.Instance.LoadScene(ESceneID.Stage);
    }

    public void CallOption()
    {
        _optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        _optionPanel.SetActive(false);
    }

    public void CallShop()
    {
        _shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        _shopPanel.SetActive(false);
    }

    public void ExitGame()
    {
        DataSave.SaveGameData();
        Application.Quit();
    }
}

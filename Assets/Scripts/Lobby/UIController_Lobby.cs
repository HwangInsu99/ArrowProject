using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_Lobby : MonoBehaviour
{
    [SerializeField] private LobbyManager _manager;

    public void ButtonGameStart() => _manager.GameStart();
    public void ButtonCallOption() => _manager.CallOption();
    public void ButtonCloseOption() => _manager.CloseOption();
    public void ButtonCallShop() => _manager.CallShop();
    public void ButtonCloseShop() => _manager.CloseShop();
    public void ButtonExitGame() => _manager.ExitGame();
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    private int _score;
    private int _money;

    void Start()
    {
        ChangeScore(0);
        SetMoney();
    }

    public void ChangeScore(int score)
    {
        _score += score;
        _scoreText.text = "Á¡¼ö: " + _score.ToString();
    }

    public void SetMoney()
    {
        _money = GameData.Instance._money;
        _moneyText.text = "µ·: " + _money.ToString();
    }
}

using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _moneyText;
    private int _score;

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
        _moneyText.text = "µ·: " + GameData.Instance.Money.ToString();
    }
}

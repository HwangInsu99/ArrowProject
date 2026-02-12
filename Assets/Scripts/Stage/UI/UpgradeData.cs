using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct StatInfo
{
    public StatType type;
    public float value;
}

public class UpgradeData : MonoBehaviour
{
    [SerializeField] private UpgradeButton[] _buttons = new UpgradeButton[3];
    [SerializeField] private UpgradeDataSO[] _statDatas;
    private int _rank;
    private int _reRollCost = 2;
    [SerializeField] private TextMeshProUGUI _costText;
    
    public void RandomStat()
    {
        _costText.text = $"-{_reRollCost}";
        if (_statDatas.Length < 3)
        {
            Debug.LogError("저장된 데이터 부족");
            return;
        }
        _rank = RandomRank();
        RandomData();
    }

    int RandomRank()
    {
        float rank3 = 0.1f;
        float rank2 = 0.3f;
        //float rank1 = 0.6f;

        float rand = Random.value;

        if (rand < rank3)
            return 3;
        else if (rand < rank3 + rank2)
            return 2;
        else
            return 1;

    }

    void RandomData()
    {
        int count = 0;

        foreach (UpgradeDataSO data in _statDatas)
        {
            if (data.Rank == _rank)
                count++;
        }

        if (count < _buttons.Length)
        {
            Debug.LogError($"랭크{_rank} 데이터 부족");
            return;
        }
        List<UpgradeDataSO> pool = new List<UpgradeDataSO>(_statDatas);

        for (int i = 0; i < _buttons.Length;)
        {
            int rand = Random.Range(0, pool.Count);
            // 확정된 랭크가 아니면 재선택 하면서 이번 값 삭제
            if (pool[rand].Rank == _rank)
            {
                _buttons[i].SetParameterValue(pool[rand]);
                i++;
            }
            pool.RemoveAt(rand);
        }
    }

    public void ButtonReRoll()
    {
        if (GameData.Instance.Money < _reRollCost)
            return;
        GameManager.Instance.ChangeMoney(-_reRollCost);
        RandomData();
    }
}

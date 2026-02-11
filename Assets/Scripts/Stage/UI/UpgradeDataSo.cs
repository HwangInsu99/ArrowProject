using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeDataSO : ScriptableObject
{
    [SerializeField] private int _rank;
    public int Rank => _rank;
    [SerializeField, TextArea(1, 3)] private string _explain;
    public string Explain => _explain;

    [SerializeField] private List<StatInfo> _info;    
    public IReadOnlyList<StatInfo> Infos => _info;

    public float HpValue()
    {
        float value;
        float min = 0;
        float max = 0;
        switch (_rank)
        {
            case 1:
                min = 1.5f;
                max = 2.0f;
                break;
            case 2:
                min = 2.6f;
                max = 3.5f;
                break;
            case 3:
                min = 4.8f;
                max = 6.0f;
                break;
        }
        float result = Random.Range(min, max);
        value = GameManager.Instance._enemyBaseHp * result;
        return value;
    }
}
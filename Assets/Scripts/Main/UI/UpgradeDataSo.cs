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
}
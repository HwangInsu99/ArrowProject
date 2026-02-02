using System.Linq;
using UnityEngine;

public class UpgradeData : MonoBehaviour
{
    [System.Serializable]
    public class Upgrade
    {
        public UpgradeButton button;
    }
    public Upgrade[] button;
    
    public void RandomStat()
    {
        var enumvalue = System.Enum.GetValues(enumType: typeof(StatType));
        StatType[] type = new StatType[3];
        for (int i = 0; i < type.Length;)
        {
            StatType now = (StatType)enumvalue.GetValue(Random.Range(0, enumvalue.Length));
            if (!type.Contains(now)){
                type[i] = now;
                i++;
            }
        }

        for (int i = 0; i < type.Length; i++)
        {
            int rank = RandomRank();
            int value = StatValue(type[i], rank);
            button[i].button.SetParameterValue(type[i], rank, value);
        }
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

    int StatValue(StatType type, int rank)
    {
        int returnNum = 0;
        switch (type)
        {
            case StatType.ArrowPower:
                returnNum = PowerRank(rank);
                break;
            case StatType.ArrowRange:
                returnNum = RangeRank(rank);
                break;
            case StatType.ArrowSpeed:
                returnNum = SpeedRank(rank);
                break;
            case StatType.AttackRate:
                returnNum = AttackRateRank(rank);
                break;
            case StatType.MoveSpeed:
                returnNum = MoveSpeedRank(rank);
                break;
            case StatType.PlayerHp:
                returnNum = HpRank(rank);
                break;
            case StatType.CriticalPer:
                returnNum = CritPerRank(rank);
                break;
        }
        return returnNum;
    }

    int PowerRank(int rank)
    {
        if (rank == 3)
            return 9;
        else if (rank == 2)
            return 5;
        else
            return 2;
    }

    int RangeRank(int rank)
    {
        if (rank == 3)
            return 10;
        else if (rank == 2)
            return 7;
        else
            return 3;
    }

    int SpeedRank(int rank)
    {
        if (rank == 3)
            return 9;
        else if (rank == 2)
            return 5;
        else
            return 2;
    }

    int AttackRateRank(int rank)
    {
        if (rank == 3)
            return 7;
        else if (rank == 2)
            return 5;
        else
            return 3;
    }

    int MoveSpeedRank(int rank)
    {
        if (rank == 3)
            return 12;
        else if (rank == 2)
            return 7;
        else
            return 4;
    }

    int HpRank(int rank)
    {
        if (rank == 3)
            return 16;
        else if (rank == 2)
            return 9;
        else
            return 3;
    }

    int CritPerRank(int rank)
    {
        if (rank == 3)
            return 12;
        else if (rank == 2)
            return 10;
        else
            return 8;
    }
}

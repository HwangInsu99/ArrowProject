using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    [SerializeField] private int _money;
    [SerializeField] private int _hp;
    [SerializeField] private int _power;
    [SerializeField] private int _rate;
    [SerializeField] private int _num;
    [SerializeField] private int _speedD;
    [SerializeField] private bool _isPenetrate;
    [SerializeField] private int _lifeSteal;
    [SerializeField] private int _crit;
    [SerializeField] private int _reduceD;

    public int Money => _money;
    public int HP => _hp;
    public int Power => _power;
    public int Rate => _rate;
    public int SwordNum => _num;
    public int SpeedD => _speedD;
    public int Crit => _crit;
    public bool Penetrate => _isPenetrate;
    public int LifeSteal => _lifeSteal;
    public int ReduceD => _reduceD;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DataSave.LoadGameData();
    }

    public void ChangeMoney(int money)
    {
        _money += money;
        DataSave.SaveGameData();
    }

    public void UpgradeHP() => _hp++;
    public void UpgradePower() => _power++;
    public void UpgradeRate() => _rate++;
    public void UpgradeSwordNum() => _num++;
    public void UpgradeSpeedD(int value) => _speedD = value;
    public void UpgradePenetrate() => _isPenetrate = true;
    public void UpgradeLifeSteal(int value) => _lifeSteal = value;
    public void UpgradeCrit(int value) => _crit = value;
    public void UpgradeReduceD(int value) => _reduceD = value;

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}

public static class DataSave
{
    private static string SavePath => Path.Combine(Application.dataPath, "SaveData.json");

    public static void SaveGameData()
    {
        // 1. 객체를 JSON 문자열로 변환 (true를 넣으면 가독성 좋게 줄바꿈 해줌)
        string json = JsonUtility.ToJson(GameData.Instance, true);

        // 2. 파일 쓰기
        File.WriteAllText(SavePath, json);
        Debug.Log("데이터 저장 완료: " + SavePath);
    }

    // [로드] 파일 내용을 읽어와서 객체에 덮어쓰기
    public static void LoadGameData()
    {
        // 파일이 존재할 때만 로드
        if (File.Exists(SavePath))
        {
            // 1. 파일 내용 읽기
            string json = File.ReadAllText(SavePath);

            // 2. JSON 문자열을 객체 데이터로 복원
            JsonUtility.FromJsonOverwrite(json, GameData.Instance);
            Debug.Log("데이터 로드 완료");
        }
        else
        {
            Debug.Log("저장된 파일이 없어 초기 데이터로 시작합니다.");
        }
    }
}

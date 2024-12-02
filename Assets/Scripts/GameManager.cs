// using UnityEngine;
// using TMPro;

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance;

//     public int playerLevel;
//     public int playerExperience;
//     public int playerCoins;
//     public int playerDiamonds;

//     public TextMeshProUGUI coinText;

//     void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     void Start()
//     {
//         LoadGame();
//         UpdateCoinUI();
//         DebugPlayerData();
//     }

//     void OnApplicationQuit()
//     {
//         SaveGame();
//     }

//     public void SaveGame()
//     {
//         PlayerPrefs.SetInt("PlayerLevel", playerLevel);
//         PlayerPrefs.SetInt("PlayerExperience", playerExperience);
//         PlayerPrefs.SetInt("PlayerCoins", playerCoins);
//         PlayerPrefs.SetInt("PlayerDiamonds", playerDiamonds);
//         PlayerPrefs.Save();

//         Debug.Log("Saved player data using PlayerPrefs");
//     }

//     public void LoadGame()
//     {
//         if (PlayerPrefs.HasKey("PlayerLevel"))
//         {
//             playerLevel = PlayerPrefs.GetInt("PlayerLevel");
//             playerExperience = PlayerPrefs.GetInt("PlayerExperience");
//             playerCoins = PlayerPrefs.GetInt("PlayerCoins");
//             playerDiamonds = PlayerPrefs.GetInt("PlayerDiamonds");

//             Debug.Log("Loaded player data using PlayerPrefs");
//         }
//         else
//         {
//             playerLevel = 1;
//             playerExperience = 0;
//             playerCoins = 0;
//             playerDiamonds = 0;
//             Debug.Log("No player data found, starting with default values.");
//         }

//         UpdateCoinUI();
//     }

//     public void DebugPlayerData()
//     {
//         Debug.Log("Player Level: " + playerLevel);
//         Debug.Log("Player Experience: " + playerExperience);
//         Debug.Log("Player Coins: " + playerCoins);
//         Debug.Log("Player Diamonds: " + playerDiamonds);
//     }

//     public void UpdateCoinUI()
//     {
//         if (coinText != null)
//         {
//             coinText.text = playerCoins.ToString();
//         }
//     }

//     public void UpdatePlayerStats(int expGain, int coinsGain, int diamondsGain)
//     {
//         playerExperience += expGain;
//         playerCoins += coinsGain;
//         playerDiamonds += diamondsGain;

//         if (playerExperience >= 100)
//         {
//             playerLevel++;
//             playerExperience = 0;
//         }

//         SaveGame();
//         UpdateCoinUI();
//     }
// }




using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤 인스턴스

    // 플레이어 데이터
    public int playerLevel;
    public int playerExperience;
    public int playerCoins;
    public int playerDiamonds;

    // 센서 데이터
    public float sensorTemperature;
    public float sensorHumidity;
    public float sensorSoilMoisture;
    public float sensorPIR;

    // UI 요소
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI experienceText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadGame();       // 플레이어 데이터 불러오기
        UpdateCoinUI();   // UI 업데이트
        UpdateLevelUI();  // 레벨 UI 업데이트
    }

    void OnApplicationQuit()
    {
        SaveGame(); // 앱 종료 시 데이터 저장
    }

    // 게임 데이터 저장
    public void SaveGame()
    {
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.SetInt("PlayerExperience", playerExperience);
        PlayerPrefs.SetInt("PlayerCoins", playerCoins);
        PlayerPrefs.SetInt("PlayerDiamonds", playerDiamonds);
        PlayerPrefs.Save();

        Debug.Log("Player data saved successfully!");
    }

    // 게임 데이터 불러오기
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            playerLevel = PlayerPrefs.GetInt("PlayerLevel");
            playerExperience = PlayerPrefs.GetInt("PlayerExperience");
            playerCoins = PlayerPrefs.GetInt("PlayerCoins");
            playerDiamonds = PlayerPrefs.GetInt("PlayerDiamonds");

            Debug.Log("Player data loaded successfully!");
        }
        else
        {
            // 기본 값 설정
            playerLevel = 1;
            playerExperience = 0;
            playerCoins = 0;
            playerDiamonds = 0;
            Debug.Log("No saved data found. Default values applied.");
        }

        UpdateCoinUI();
        UpdateLevelUI();
    }

    // 코인 UI 업데이트
    public void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = $"Coins: {playerCoins}";
    }

    // 레벨 및 경험치 UI 업데이트
    public void UpdateLevelUI()
    {
        if (levelText != null)
            levelText.text = $"Level: {playerLevel}";
        if (experienceText != null)
            experienceText.text = $"Experience: {playerExperience}/100";
    }

    // 플레이어 경험치 및 레벨 업데이트
    public void UpdatePlayerStats(int expGain, int coinsGain, int diamondsGain)
    {
        playerExperience += expGain;
        playerCoins += coinsGain;
        playerDiamonds += diamondsGain;

        // 레벨업 로직
        if (playerExperience >= 100)
        {
            playerLevel++;
            playerExperience -= 100; // 잉여 경험치는 다음 레벨로 이월
            Debug.Log("Level up! Current level: " + playerLevel);
        }

        SaveGame(); // 데이터 저장
        UpdateCoinUI(); // UI 업데이트
        UpdateLevelUI(); // UI 업데이트
    }

    // 디버그 로그를 통한 플레이어 데이터 확인
    public void DebugPlayerData()
    {
        Debug.Log($"Player Level: {playerLevel}");
        Debug.Log($"Player Experience: {playerExperience}");
        Debug.Log($"Player Coins: {playerCoins}");
        Debug.Log($"Player Diamonds: {playerDiamonds}");
        Debug.Log($"Sensor Data - Temp: {sensorTemperature}, Humidity: {sensorHumidity}, Soil Moisture: {sensorSoilMoisture}, PIR: {sensorPIR}");
    }
}

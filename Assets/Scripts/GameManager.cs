using UnityEngine;
using TMPro;
using System.Collections;

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
        StartCoroutine(InitializeGame());
    }

    IEnumerator InitializeGame()
    {
        // InventoryManager가 초기화될 때까지 대기
        yield return new WaitUntil(() => InventoryManager.Instance != null);
        
        LoadGame();
        UpdateCoinUI();
        UpdateLevelUI();
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

    // 인벤토리에 아이템이 있을 때만 저장
        if (InventoryManager.Instance != null && InventoryManager.Instance.inventoryItems.Count > 0)
        {
            for (int i = 0; i < InventoryManager.Instance.inventoryItems.Count; i++)
            {
                Item item = InventoryManager.Instance.inventoryItems[i];
                PlayerPrefs.SetString("ItemName" + i, item.itemName);
                PlayerPrefs.SetInt("ItemCount" + i, item.itemCount);
                PlayerPrefs.SetString("ItemDescription" + i, item.itemDescription);
            }
        }
        
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        playerExperience = PlayerPrefs.GetInt("PlayerExperience", 0);
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        playerDiamonds = PlayerPrefs.GetInt("PlayerDiamonds", 0);

        // 인벤토리가 존재하고 저장된 아이템이 있을 때만 로드
        if (InventoryManager.Instance != null && PlayerPrefs.HasKey("ItemName0"))
        {
            InventoryManager.Instance.inventoryItems.Clear();
            
            int i = 0;
            while (PlayerPrefs.HasKey("ItemName" + i))
            {
                string itemName = PlayerPrefs.GetString("ItemName" + i);
                int itemCount = PlayerPrefs.GetInt("ItemCount" + i);
                string itemDescription = PlayerPrefs.GetString("ItemDescription" + i);

                Item item = new Item(itemName, itemDescription, itemCount);
                InventoryManager.Instance.inventoryItems.Add(item);
                i++;
            }
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
    }
}

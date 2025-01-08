using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
        public Button oneGacha;
    public Button tenGacha;

    private bool isOneCoin = false;
    private bool isTenCoin = false;

    // 아이템 이름과 확률, 그리고 설명 데이터
    private Dictionary<string, float> items = new Dictionary<string, float>()
    {
        { "사과", 1f },
        { "바나나", 5f},
        { "딸기", 14f},
        { "키위", 30f},
        { "당근", 50f}
    };

    private List<string> gachaPool;

    void Start()
    {
        oneGacha.interactable = false;
        tenGacha.interactable = false;

        // 가챠 풀 생성
        gachaPool = CreateGachaPool();
    }

    private void Update()
    {
        CheckCoin();
    }

    void CheckCoin()
    {
        if (GameManager.Instance.playerCoins >= 1)
        {
            isOneCoin = true;
            oneGacha.interactable = true;
        }
        else
        {
            isOneCoin = false;
            oneGacha.interactable = false;
        }

        if (GameManager.Instance.playerCoins >= 5)
        {
            isTenCoin = true;
            tenGacha.interactable = true;
        }
        else
        {
            isTenCoin = false;
            tenGacha.interactable = false;
        }
    }

    // 가챠 풀 생성
    private List<string> CreateGachaPool()
    {
        List<string> pool = new List<string>();

        foreach (var item in items)
        {
            int count = Mathf.RoundToInt(item.Value * 10); // 정밀도를 높이기 위해 10배로 설정
            for (int i = 0; i < count; i++)
            {
                pool.Add(item.Key);
            }
        }

        return pool;
    }

    public void OneCoin()
    {
        if (isOneCoin) // boolean 값만 체크
        {
            GameManager.Instance.playerCoins -= 1;
            GameManager.Instance.UpdateCoinUI();
            RewardGacha(1); // 1회 뽑기
            oneGacha.interactable = false;
            isOneCoin = false;
        }
    }

    public void TenCoin()
    {
        if (isTenCoin) // boolean 값만 체크
        {
            GameManager.Instance.playerCoins -= 5;
            GameManager.Instance.UpdateCoinUI();
            RewardGacha(5); // 5회 뽑기
            tenGacha.interactable = false;
            isTenCoin = false;
        }
    }

    void RewardGacha(int times)
    {
        for (int i = 0; i < times; i++)
        {
            string selectedItemName = gachaPool[Random.Range(0, gachaPool.Count)];
            int randomEa = Random.Range(1, 6);


            Item newItem = new Item(selectedItemName, randomEa);

            InventoryManager.Instance.AddItem(newItem);

            Debug.Log($"뽑힌 아이템: {selectedItemName} x{randomEa}");
        }

    }
}

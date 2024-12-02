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

    Item items;

    void Start()
    {
        oneGacha.interactable = false;
        tenGacha.interactable = false;
    }
    
    private void Update() {
        checkCoin();
    }
    
    void checkCoin() {
        if(GameManager.Instance.playerCoins >= 1) {
            isOneCoin = true;
            oneGacha.interactable = true;
        }
        else {
            isOneCoin = false;
        }
        if(GameManager.Instance.playerCoins >= 3) {
            isTenCoin = true;
            tenGacha.interactable = true;
        }
        else {
            isTenCoin = false;
        }
    }

    public void OneCoin(Button button)
    {
        if (button == isOneCoin && oneGacha)
        {
            GameManager.Instance.playerCoins -= 1;
            GameManager.Instance.UpdateCoinUI();
            RewardGacha();
            oneGacha.interactable = false;
            isOneCoin = false;
        }
    }

    public void TenCoin(Button button) {
        if (button == isTenCoin && tenGacha)
        {
            GameManager.Instance.playerCoins -= 3;
            GameManager.Instance.UpdateCoinUI();
            for(int i=0; i<3; i++){
                RewardGacha();
            }
            tenGacha.interactable = false;
            isTenCoin = false;
        }
    }

    void RewardGacha() 
    {
        // UnityEngine.Random을 사용하여 랜덤 값을 생성
        int randomInt = Random.Range(1, 4);  // 1부터 3까지의 랜덤값 생성

        switch(randomInt) 
        {
            case 1:
                InventoryManager.Instance.AddItem(new Item("사과", "맛있다", 2));
                break;
            case 2:
                InventoryManager.Instance.AddItem(new Item("바나나", "맛있다", 3));
                break;
            case 3:
                InventoryManager.Instance.AddItem(new Item("딸기", "맛있다", 4));
                break;
        }
    }

}

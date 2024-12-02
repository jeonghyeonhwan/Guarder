using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static InventoryManager Instance { get; private set; } // 다른 클래스에서 인스턴스를 임의로 조정 불가

    // 아이템 목록
    public List<Item> inventoryItems = new List<Item>();
    
    // [SerializeField]
    // private UIManager uiManager;

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            // 이 오브젝트를 씬 전환 시 파괴X
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 다른 인스턴스가 존재하면 현재 게임 오브젝트를 파괴
            Destroy(gameObject);
        }
    }

    // 아이템 획득 시 
    public void AddItem(Item getItem)
    {
        inventoryItems.Add(getItem);
        Debug.Log($"아이템 추가됨: {getItem.itemName}");
        // UI 갱신
        // uiManager.UpdateInventoryUI(inventoryItems);
    }

    // 아이템 제거
    public void RemoveItem(Item removeItem)
    {
        if (inventoryItems.Contains(removeItem))
        {
            inventoryItems.Remove(removeItem);
            Debug.Log($"아이템 제거됨: {removeItem.itemName}");
            DebugLogInventoryItems(); // 인벤토리 목록 체크
            // UI 갱신
            // uiManager.UpdateInventoryUI(inventoryItems);
        }
    }


    // 인벤토리의 모든 아이템을 디버그 로그로 출력
    public void DebugLogInventoryItems()
    {
        if (inventoryItems.Count == 0)
        {
            Debug.Log("인벤토리가 비어 있습니다.");
        }
        else
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                Debug.Log($"아이템 {i + 1}: 이름 = {inventoryItems[i].itemName}, 설명 = {inventoryItems[i].itemDescription}, 개수 = {inventoryItems[i].itemCount}");
            }
        }
    }
}
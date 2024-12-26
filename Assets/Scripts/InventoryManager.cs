using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static InventoryManager Instance { get; private set; }

    // 아이템 목록
    public List<Item> inventoryItems = new List<Item>();

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 리스트 초기화
            if (inventoryItems == null)
            {
                inventoryItems = new List<Item>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 아이템 추가
    public void AddItem(Item getItem)
    {
        // 방어 코드: null 검사
        if (getItem == null || string.IsNullOrEmpty(getItem.itemName))
        {
            Debug.LogError("추가하려는 아이템이 null이거나 itemName이 유효하지 않습니다.");
            return;
        }

        // 동일한 아이템이 존재하는지 확인
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i].itemName == getItem.itemName)
            {
                // 수량 업데이트
                inventoryItems[i].itemCount += getItem.itemCount;

                return;
            }
        }

        // 새로운 아이템 추가
        inventoryItems.Add(getItem);
        Debug.Log($"아이템 추가됨: {getItem.itemName}");
    }

    // 아이템 제거
    public void RemoveItem(Item removeItem)
    {
        // 방어 코드
        if (removeItem == null || string.IsNullOrEmpty(removeItem.itemName))
        {
            Debug.LogError("제거하려는 아이템이 null이거나 itemName이 유효하지 않습니다.");
            return;
        }

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i] != null && inventoryItems[i].itemName == removeItem.itemName)
            {
                inventoryItems[i].itemCount -= removeItem.itemCount;

                if (inventoryItems[i].itemCount <= 0)
                {
                    inventoryItems.RemoveAt(i); // 수량이 0 이하인 경우 리스트에서 제거
                }

                Debug.Log($"아이템 제거됨: {removeItem.itemName}");
                // DebugLogInventoryItems();
                return;
            }
        }

        Debug.LogWarning($"제거하려는 아이템이 인벤토리에 없습니다: {removeItem.itemName}");
    }

    // 디버그: 현재 인벤토리 상태 출력
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
                if (inventoryItems[i] != null)
                {
                    Debug.Log($"아이템 {i + 1}: 이름 = {inventoryItems[i].itemName}, 설명 = {inventoryItems[i].itemDescription}, 개수 = {inventoryItems[i].itemCount}");
                }
            }
        }
    }

    // 인벤토리 초기화
    public void ClearInventory()
    {
        inventoryItems.Clear();
        Debug.Log("인벤토리 초기화 완료");
        DebugLogInventoryItems();
    }
}
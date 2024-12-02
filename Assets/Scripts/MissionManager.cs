// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class MissionManager : MonoBehaviour
// {
//     public Button missionButton1; // 일일 미션 버튼
//     public Button missionButton2;
//     public Button missionButton3;

//     private bool isConditionMet1 = false; // 조건이 충족되었는지 여부
//     private bool isConditionMet2 = false;
//     private bool isConditionMet3 = false;

//     void Start()
//     {
//         // 버튼 초기 상태 설정
//         missionButton1.interactable = false;
//         missionButton2.interactable = false;
//         missionButton3.interactable = false; // 처음에는 비활성화 상태
//         GameManager.Instance.UpdateCoinUI(); // GameManager에서 코인 UI 업데이트
//     }

//     void Update()
//     {
//         // 조건 체크 (예시로 임의 조건을 사용함)
//         CheckMissionCondition1();
//         CheckMissionCondition2();
//         CheckMissionCondition3();

//         // 조건이 충족되면 버튼 활성화
//         if (isConditionMet1)
//         {
//             missionButton1.interactable = true;
//         }
//         else
//         {
//             missionButton1.interactable = false;
//         }
//         if (isConditionMet2)
//         {
//             missionButton2.interactable = true;
//         }
//         else
//         {
//             missionButton2.interactable = false;
//         }
//         if (isConditionMet3)
//         {
//             missionButton3.interactable = true;
//         }
//         else
//         {
//             missionButton3.interactable = false;
//         }
//     }

//     // 미션 조건을 확인하는 함수
//     void CheckMissionCondition1()
//     {
//         if (GameManager.Instance.playerCoins <= 200) // 조건 적기.
//         {
//             isConditionMet1 = true;
//         }
//         else
//         {
//             isConditionMet1 = false;
//         }
//     }
//         void CheckMissionCondition2()
//     {
//         if (GameManager.Instance.playerCoins <= 230)
//         {
//             isConditionMet2 = true;
//         }
//         else
//         {
//             isConditionMet2 = false;
//         }
//     }
//         void CheckMissionCondition3()
//     {
//         if (GameManager.Instance.playerCoins <= 250)
//         {
//             isConditionMet3 = true;
//         }
//         else
//         {
//             isConditionMet3 = false;
//         }
//     }
//     // 버튼을 클릭했을 때 보상을 지급하는 함수
//     public void OnMissionButtonClick()
//     {
//         if (isConditionMet1)
//         {
//             GameManager.Instance.playerCoins += 1; // 코인 증가
//             GameManager.Instance.UpdateCoinUI(); // UI 업데이트
//             GameManager.Instance.SaveGame(); // 게임 데이터 저장

//             // 보상 지급 후 버튼 비활성화
//             missionButton1.interactable = false;
//             isConditionMet1 = false;
//         }
//         else if (isConditionMet2)
//         {
//             GameManager.Instance.playerCoins += 1; // 코인 증가
//             GameManager.Instance.UpdateCoinUI(); // UI 업데이트
//             GameManager.Instance.SaveGame(); // 게임 데이터 저장

//             // 보상 지급 후 버튼 비활성화
//             missionButton2.interactable = false;
//             isConditionMet2 = false;
//         }
//         else if(isConditionMet3)
//         {
//             GameManager.Instance.playerCoins += 1; // 코인 증가
//             GameManager.Instance.UpdateCoinUI(); // UI 업데이트
//             GameManager.Instance.SaveGame(); // 게임 데이터 저장

//             // 보상 지급 후 버튼 비활성화
//             missionButton3.interactable = false;
//             isConditionMet3 = false;
//         }
//     }
// }


using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public Button missionButton1;
    public Button missionButton2;
    public Button missionButton3;

    private bool isConditionMet1 = false;
    private bool isConditionMet2 = false;
    private bool isConditionMet3 = false;

    void Start()
    {
        missionButton1.interactable = false;
        missionButton2.interactable = false;
        missionButton3.interactable = false;
        GameManager.Instance.UpdateCoinUI();
    }

    void Update()
    {
        CheckMissionCondition1();
        CheckMissionCondition2();
        CheckMissionCondition3();

        missionButton1.interactable = isConditionMet1;
        missionButton2.interactable = isConditionMet2;
        missionButton3.interactable = isConditionMet3;
    }

    void CheckMissionCondition1()
    {
        if (GameManager.Instance.sensorTemperature >= 25)
            isConditionMet1 = true;
        else
            isConditionMet1 = false;
    }

    void CheckMissionCondition2()
    {
        if (GameManager.Instance.sensorHumidity >= 50)
            isConditionMet2 = true;
        else
            isConditionMet2 = false;
    }

    void CheckMissionCondition3()
    {
        if (GameManager.Instance.sensorSoilMoisture >= 700)
            isConditionMet3 = true;
        else
            isConditionMet3 = false;
    }

    public void OnMissionButtonClick(Button button)
    {
        if (button == missionButton1 && isConditionMet1)
        {
            RewardPlayer(1);
            missionButton1.interactable = false;
            isConditionMet1 = false;
        }
        else if (button == missionButton2 && isConditionMet2)
        {
            RewardPlayer(1);
            missionButton2.interactable = false;
            isConditionMet2 = false;
        }
        else if (button == missionButton3 && isConditionMet3)
        {
            RewardPlayer(1);
            missionButton3.interactable = false;
            isConditionMet3 = false;
        }
    }

    void RewardPlayer(int coins)
    {
        GameManager.Instance.playerCoins += coins;
        GameManager.Instance.SaveGame();
        GameManager.Instance.UpdateCoinUI();
    }
}

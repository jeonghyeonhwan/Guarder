using UnityEngine;
using UnityEngine.UI;

public class DailyQuestManager : MonoBehaviour
{
    public Button questButton1;
    public Button questButton2;
    public Button questButton3;

    private bool isQuest1Complete = false;
    private bool isQuest2Complete = false;
    private bool isQuest3Complete = false;

    void Start()
    {
        questButton1.onClick.AddListener(() => CompleteQuest(1));
        questButton2.onClick.AddListener(() => CompleteQuest(2));
        questButton3.onClick.AddListener(() => CompleteQuest(3));

        questButton1.interactable = false;
        questButton2.interactable = false;
        questButton3.interactable = false;
    }

    void Update()
    {
        // 퀘스트 완료 조건 설정
        if (isQuest1Complete)
        {
            questButton1.interactable = true;
        }

        if (isQuest2Complete)
        {
            questButton2.interactable = true;
        }

        if (isQuest3Complete)
        {
            questButton3.interactable = true;
        }
    }

    public void CompleteQuest(int questNumber)
    {
        if (questNumber == 1 && isQuest1Complete)
        {
            GameManager.Instance.UpdatePlayerStats(0, 10, 0);
            questButton1.interactable = false;
        }
        else if (questNumber == 2 && isQuest2Complete)
        {
            GameManager.Instance.UpdatePlayerStats(0, 20, 0);
            questButton2.interactable = false;
        }
        else if (questNumber == 3 && isQuest3Complete)
        {
            GameManager.Instance.UpdatePlayerStats(0, 30, 0);
            questButton3.interactable = false;
        }
    }
}

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryManager : MonoBehaviour
{
    public TMP_InputField diaryInput;
    public TextMeshProUGUI diaryDisplay;
    private string filePath;
    private DiaryData diaryData = new DiaryData();

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "diary.json");
        LoadDiary();
    }

    public void SaveEntry()
    {
        DiaryEntry newEntry = new DiaryEntry
        {
            date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            content = diaryInput.text
        };

        diaryData.entries.Add(newEntry);
        File.WriteAllText(filePath, JsonUtility.ToJson(diaryData));
        diaryInput.text = "";
        DisplayDiary();
    }

    void LoadDiary()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            diaryData = JsonUtility.FromJson<DiaryData>(json);
            DisplayDiary();
        }
    }

    void DisplayDiary()
    {
        diaryDisplay.text = "";
        foreach (var entry in diaryData.entries)
        {
            diaryDisplay.text += $"[{entry.date}]\n{entry.content}\n\n";
        }
    }
}


[System.Serializable]
public class DiaryEntry
{
    public string date;
    public string content;
}

[System.Serializable]
public class DiaryData
{
    public List<DiaryEntry> entries = new List<DiaryEntry>();
}
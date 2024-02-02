using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    [SerializeField] private GameObject achievementPrefab; 
    [SerializeField] private Transform achievementsContent;
    [SerializeField] private TextMeshProUGUI achievementTemp;
    

    [SerializeField] GameObject AchPanel;
    private bool isAchPanel=false;

    private int towersBuilt = 0;


    public List<Achievement> Achievements = new List<Achievement>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        WaveManager.EnemiesKilled += HandleEnemiesKilled;
        TurretButton.TurretCreated += HandleTurretCreated;
        WaveManager.WaveEnded += HandleWaveEnded;
    }

    private void OnDestroy()
    {
        WaveManager.EnemiesKilled -= HandleEnemiesKilled; 
        TurretButton.TurretCreated -= HandleTurretCreated;
        WaveManager.WaveEnded -= HandleWaveEnded;
    }

    private void HandleEnemiesKilled(int enemiesKilled)
    {
        if (enemiesKilled % 5 == 0 && enemiesKilled > 0) 
        {
            string achievementKey = $"EnemiesKilled_{enemiesKilled}";
            GrantAchievement(achievementKey, $"Убито {enemiesKilled} врагов!");
        }
    }

    private void HandleTurretCreated()
    {
        towersBuilt++;
        if (towersBuilt % 5 == 0 && towersBuilt > 0) 
        {
            string achievementKey = $"towersBuilt_{towersBuilt}";
            GrantAchievement(achievementKey, $"Построено {towersBuilt} башень!");
        }
    }

    private void HandleWaveEnded(int waveIndex)
    {
        string achievementKey = $"waveEnd_{waveIndex}";
        GrantAchievement(achievementKey, $"Прошли {waveIndex} волну!");
    }

    public void GrantAchievement(string achievementKey, string message)
    {
        Achievement achievement = Achievements.Find(a => a.Name == achievementKey);
        if (achievement == null)
        {
            achievement = new Achievement(achievementKey, message);
            Achievements.Add(achievement);
        }

        if (!achievement.Achieved)
        {
            achievement.Achieved = true;
            Debug.Log($"Достижение получено: {achievement.Description}");
            achievementTemp.text=achievement.Description;
            
            AddAchievementToUI(achievement); 
        }
    }


    public void AddAchievementToUI(Achievement achievement)
    {
        if (!achievement.Achieved) return;
        GameObject achievementItem = Instantiate(achievementPrefab, achievementsContent);
        AchievementItemUI itemUI = achievementItem.GetComponent<AchievementItemUI>();
        if (itemUI != null)
        {
            itemUI.Setup(achievement.Name, achievement.Description);
        }
    }

    public void DisplayAch()
    {
        isAchPanel=!isAchPanel;
        AchPanel.SetActive(isAchPanel);
    }

}

[System.Serializable]
public class Achievement
{
    public string Name; 
    public string Description; 
    public bool Achieved;

    public Achievement(string name, string description)
    {
        Name = name;
        Description = description;
        Achieved = false;
    }
}
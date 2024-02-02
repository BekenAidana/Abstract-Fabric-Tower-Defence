
using UnityEngine;
using TMPro;

public class AchievementItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void Setup(string title, string description)
    {
        Debug.Log("title");
        gameObject.name = title;
        descriptionText.text = description;
    }
}

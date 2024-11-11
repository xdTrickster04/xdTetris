using UnityEngine;
using TMPro; // TextMeshPro namespace

public class LevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText; // Reference to the TextMeshProUGUI component
    public BoardHard board; // Reference to the BoardHard

    private void Update()
    {
        if (board != null && levelText != null)
        {
            levelText.text = "Level: " + board.GetLevel().ToString();
        }
    }
}

using UnityEngine;

public class MenuManager : MonoBehaviour {
    public GameObject mainMenu; // Reference to the Main Menu panel
    public GameObject gameOverPanel; // Reference to the Game Over panel

    void Start() {
        // Check if the Game Over parameter is set
        if (PlayerPrefs.GetInt("GameOver", 0) == 1) {
            ShowGameOver();
        } else {
            ShowMainMenu();
        }
        
        // Reset the Game Over parameter
        PlayerPrefs.SetInt("GameOver", 0);
    }

    void ShowGameOver() {
        mainMenu.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    void ShowMainMenu() {
        mainMenu.SetActive(true);
        gameOverPanel.SetActive(false);
    }
}

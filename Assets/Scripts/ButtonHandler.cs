using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {
    // Method to load the Main Menu scene
    public void ReturnToMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    // Method to restart the game scene (if needed)
    public void PlayAgain() {
        string selectedDifficulty = PlayerPrefs.GetString("SelectedDifficulty", "Easy");

        if (selectedDifficulty == "Easy") {
            SceneManager.LoadScene("xdTetris");
        } else if (selectedDifficulty == "Hard") {
            SceneManager.LoadScene("xdTetris 1");
        }
    }
}

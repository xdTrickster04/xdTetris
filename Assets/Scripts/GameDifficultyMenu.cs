using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDifficultyMenu : MonoBehaviour {
    public void LoadEasyDifficulty() {
        PlayerPrefs.SetString("SelectedDifficulty", "Easy");
        SceneManager.LoadScene("xdTetris");
    }

    public void LoadHardDifficulty() {
        PlayerPrefs.SetString("SelectedDifficulty", "Hard");
        SceneManager.LoadScene("xdTetris 1");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    public Slider volumeSlider;

    void Start() {
        // Load the saved volume value
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
    }

    public void OnVolumeChanged(float value) {
        // Save the new volume value
        PlayerPrefs.SetFloat("Volume", value);
        // Apply the volume change
        BGMusic.instance.SetVolume(value);
    }
}

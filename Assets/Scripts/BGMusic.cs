using UnityEngine;

public class BGMusic : MonoBehaviour {
    public static BGMusic instance;
    private AudioSource audioSource;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        // Set the initial volume from saved preferences
        SetVolume(PlayerPrefs.GetFloat("Volume", 1.0f));
    }

    public void SetVolume(float value) {
        audioSource.volume = value;
    }
}

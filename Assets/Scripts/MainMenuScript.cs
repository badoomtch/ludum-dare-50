using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.Play();
        volumeSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMusic.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

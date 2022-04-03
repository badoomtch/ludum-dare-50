using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject deathScreenUI;
    public TextMeshProUGUI winDeathText;

    public bool hasWon;
    public bool isPaused;

    public GameObject playerCamera;
    public GameObject player;
    public GameObject pauseMenu;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;

    public Slider sensSlider;
    public Slider fovSlider;
    public Slider volumeSlider;
    public float sensValue;
    public float gameVolume;
    public float fovValue;

    private Animation anim;
    public GameObject blackFadePanel;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        blackFadePanel.SetActive(false);
        sensSlider.value = player.GetComponent<SUPERCharacter.SUPERCharacterAIO>().Sensitivity;
        fovSlider.value = playerCamera.GetComponent<Camera>().fieldOfView;

        deathScreenUI.SetActive(false);

        Time.timeScale = 1;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerCamera.GetComponent<Camera>().fieldOfView = fovSlider.value;
        player.GetComponent<SUPERCharacter.SUPERCharacterAIO>().Sensitivity = sensSlider.value;

         if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                playerWin();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isPaused = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PlayerDeath()
    {
        /* Time.timeScale = 0;
        deathScreenUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        blackFadePanel.SetActive(true); */
    }

    public void playerWin()
    {
        blackFadePanel.SetActive(true);
        Debug.Log("Player has won");
        StartCoroutine(WaitToWin());
    }

    IEnumerator WaitToWin()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
        winDeathText.text = "You survived untill the SS Icebreaker arrived. Sadly it didn't stop in time and you perished. \n You managed your time and resources well!";
        deathScreenUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

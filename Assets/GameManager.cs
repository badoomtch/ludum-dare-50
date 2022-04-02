using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject deathScreenUI;
    // Start is called before the first frame update
    void Start()
    {
        deathScreenUI.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerDeath()
    {
        Time.timeScale = 0;
        deathScreenUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void startGame()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTouch : MonoBehaviour
{
    public GameObject gameManager;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            gameManager.GetComponent<GameManager>().winDeathText.text = "That swim was a bit too cold to survive";
            gameManager.GetComponent<GameManager>().PlayerDeath();
        }
    }
}

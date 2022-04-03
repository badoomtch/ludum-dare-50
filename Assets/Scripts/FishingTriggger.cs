using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishingTriggger : MonoBehaviour
{
    public bool isNearFishing;

    public GameObject fishingGame;
    public GameObject fishingInstructions;

    void Start()
    {
        fishingGame.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Entered fishing");
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            isNearFishing = true;
            fishingGame.SetActive(true);
            fishingInstructions.SetActive(true);
        } 
    }


    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Left fishing");
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            isNearFishing = false;
            fishingGame.SetActive(true);
            fishingInstructions.SetActive(false);

            fishingGame.GetComponent<FishingMinigame>().reelingFish = false; //No longer reeling in a fish
            //Reset the thought bubbles
            fishingGame.GetComponent<FishingMinigame>().thoughtBubbles.SetActive(false);
            fishingGame.GetComponent<FishingMinigame>().thoughtBubbles.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            fishingGame.GetComponent<FishingMinigame>().minigameCanvas.SetActive(false); //Disable the fishing canvas
            fishingGame.GetComponent<FishingMinigame>().catchingbar.transform.localPosition = fishingGame.GetComponent<FishingMinigame>().catchingBarLoc; //Reset the catching bars position
        }
    }
}

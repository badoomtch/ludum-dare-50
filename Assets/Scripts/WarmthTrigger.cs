using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmthTrigger : MonoBehaviour
{
    public bool isInIgloo;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Entered the trigger");
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            isInIgloo = true;
            StopCoroutine(other.GetComponent<playerStats>().warmthDown());
            StartCoroutine(other.GetComponent<playerStats>().warmthUp());
        } 
    }


    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Left the trigger");
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            isInIgloo = false;
            StopCoroutine(other.GetComponent<playerStats>().warmthUp());
            StartCoroutine(other.GetComponent<playerStats>().warmthDown());
        }
    }
}

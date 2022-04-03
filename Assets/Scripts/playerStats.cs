using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{
    public GameObject gameManager;

    public float hungerLevel;
    public float hungerLevelMax;
    public float warmthLevel;
    public float warmthMax;

    public Slider hungerSlider;
    public Slider warmthSlider;

    public GameObject warmthTrigger;
    public GameObject furnace;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hungerDown());
        StartCoroutine(warmthDown());
        hungerLevel = hungerLevelMax;
        warmthLevel = warmthMax;
    }

    // Update is called once per frame
    void Update()
    {
        hungerSlider.value = hungerLevel;
        warmthSlider.value = warmthLevel;
    }

    public void eatFood(float healAmount)
    {
        hungerLevel += healAmount;
        if (hungerLevel > hungerLevelMax)
        {
            hungerLevel = hungerLevelMax;
        }
    }

    IEnumerator hungerDown()
    {
        yield return new WaitForSeconds(1f);
        hungerLevel--;
        StopCoroutine(hungerDown());
        StartCoroutine(hungerDown());
        if (hungerLevel <= 0)
        {
            gameManager.GetComponent<GameManager>().winDeathText.text = "You died from hunger. \n Did you know there's a chance to catch more than 1 fish at a time?";
            gameManager.GetComponent<GameManager>().PlayerDeath();
        }
    }

    public IEnumerator warmthDown()
    {
        if(warmthTrigger.GetComponent<WarmthTrigger>().isInIgloo == false || !furnace.GetComponent<cookingScript>().furnaceOn)
        {
            warmthLevel--;
            yield return new WaitForSeconds(1f);
            StopCoroutine(warmthDown());
            StartCoroutine(warmthDown());
            if (warmthLevel <= 0)
            {
                gameManager.GetComponent<GameManager>().winDeathText.text = "You froze solid. \n I would recommend cutting trees and using them for warmth by your furnace.";
                gameManager.GetComponent<GameManager>().PlayerDeath();
            }
        }
    }

    public IEnumerator warmthUp()
    {
        if(warmthTrigger.GetComponent<WarmthTrigger>().isInIgloo == true && furnace.GetComponent<cookingScript>().furnaceOn == true)
        {
            if (warmthLevel < warmthMax)
            {
                yield return new WaitForSeconds(2f);
                warmthLevel += 2f;
                StopCoroutine(warmthUp());
                StartCoroutine(warmthUp());
            }
        }
    }
}

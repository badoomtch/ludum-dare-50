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
        yield return new WaitForSeconds(2f);
        hungerLevel--;
        StartCoroutine(hungerDown());
        if (hungerLevel <= 0)
        {
            gameManager.GetComponent<GameManager>().PlayerDeath();
        }
    }

    public IEnumerator warmthDown()
    {
        if(warmthTrigger.GetComponent<WarmthTrigger>().isInIgloo == false || !furnace.GetComponent<cookingScript>().furnaceOn)
        {
            warmthLevel--;
            yield return new WaitForSeconds(1f);
            StartCoroutine(warmthDown());
            if (warmthLevel <= 0)
            {
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
                yield return new WaitForSeconds(1f);
                warmthLevel += 2f;
                StartCoroutine(warmthUp());
            }
        }
    }
}

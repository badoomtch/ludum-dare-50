using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{
    public float hungerLevel;
    public float hungerLevelMax;
    public float warmthLevel;
    public float warmthMax;

    public Slider hungerSlider;
    public Slider warmthSlider;

    public GameObject warmthTrigger;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hungerDown());
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
    }

    public IEnumerator warmthDown()
    {
        if(warmthTrigger.GetComponent<WarmthTrigger>().isWarm == false)
        {
            warmthLevel--;
            yield return new WaitForSeconds(1f);
            StartCoroutine(warmthDown());
        }
    }

    public IEnumerator warmthUp()
    {
        if(warmthTrigger.GetComponent<WarmthTrigger>().isWarm == true)
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

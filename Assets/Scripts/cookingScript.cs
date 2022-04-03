using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingScript : MonoBehaviour
{
    public bool isCooking;
    public bool hasCooked;
    public bool hasBurned;

    public bool furnaceOn;


    public float fuelValue;

    public Transform fishOut;

    public GameObject furnaceLight;
    public GameObject cookingFish;
    public GameObject cookingParticle;

    public GameObject player;
    public GameObject warmthTrigger;

    //Instantiatable objects
    public Rigidbody cookedFish;

    // Start is called before the first frame update
    void Start()
    {
        furnaceLight.SetActive(false);
        cookingFish.SetActive(false);
        cookingParticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StopCooking()
    {
        cookingFish.SetActive(false);
        cookingParticle.SetActive(false);
        isCooking = false;
        cookingFish.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        StopCoroutine(startCooking());
        StopCoroutine(startBurning());
    }

    public IEnumerator startCooking()
    {
        if (fuelValue > 0)
        {
            cookingFish.SetActive(true);
            cookingParticle.SetActive(true);
            float random = Random.Range(10f,21f);
            yield return new WaitForSeconds(random);
            cookingFish.GetComponent<Renderer>().material.color = new Color32(255, 152, 0, 255);
            hasCooked = true;
            StartCoroutine(startBurning());
        }
    }

    public IEnumerator startBurning()
    {
        if(hasCooked)
        {
            yield return new WaitForSeconds(10f);
            cookingFish.GetComponent<Renderer>().material.color = new Color32(39, 22, 22, 255);
            hasCooked = false;
            hasBurned = true;
            furnaceLight.SetActive(false);
            cookingParticle.SetActive(false);
        }
    }

    public void PrepareCookedFish()
    {
        Debug.Log("Preparing cooked fish");
        Rigidbody clone;
        clone = Instantiate(cookedFish, fishOut.position, Quaternion.identity);
        StopCooking();
    }

    public IEnumerator StartFurnance()
    {
        if (fuelValue > 0)
        {
            furnaceOn = true;
            furnaceLight.SetActive(true);
            yield return new WaitForSeconds(1f);
            fuelValue--;
            Debug.Log("Fuel " + fuelValue);
            StartCoroutine(StartFurnance());
        }
        else if (fuelValue <= 0)
        {
            furnaceOn = false;
            StopCoroutine(StartFurnance());
            furnaceLight.SetActive(false);
            StartCoroutine(player.GetComponent<playerStats>().warmthDown()); 
        }
    }
}

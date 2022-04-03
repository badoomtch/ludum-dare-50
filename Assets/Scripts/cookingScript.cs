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
        cookingFish.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StopCooking()
    {
        StopCoroutine(startCooking());
        StopCoroutine(startBurning());
        cookingFish.SetActive(false);
        cookingParticle.SetActive(false);
        isCooking = false;
        hasCooked = false;
        hasBurned = false;
        cookingFish.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
    }

    public IEnumerator startCooking()
    {
        if (fuelValue > 0)
        {
            cookingFish.SetActive(true);
            cookingParticle.SetActive(true);
            float random = Random.Range(5f,11f);
            yield return new WaitForSeconds(random);
            cookingFish.GetComponent<Renderer>().material.color = new Color32(255, 152, 0, 255);
            hasCooked = true;
            yield return new WaitForSeconds(3f);
            StopCoroutine(startBurning());
            StartCoroutine(startBurning());
            StopCoroutine(startCooking());
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
        StopCoroutine(StartFurnance());
        if (fuelValue > 0)
        {
            furnaceOn = true;
            furnaceLight.SetActive(true);
            yield return new WaitForSeconds(2f);
            fuelValue--;
            Debug.Log("Fuel " + fuelValue);
            StopCoroutine(StartFurnance());
            StartCoroutine(StartFurnance());
        }
        else if (fuelValue <= 0)
        {
            furnaceOn = false;
            isCooking = false;
            hasBurned = false;
            hasCooked = false;
            StopCoroutine(StartFurnance());
            furnaceLight.SetActive(false);
            StopCoroutine(player.GetComponent<playerStats>().warmthDown()); 
            StartCoroutine(player.GetComponent<playerStats>().warmthDown()); 
        }
    }
}

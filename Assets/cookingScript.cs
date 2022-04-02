using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingScript : MonoBehaviour
{
    public bool isCooking;
    public bool hasCooked;
    public bool hasBurned;
    public Transform fishOut;

    public GameObject furnaceLight;

    public GameObject cookingFish;

    //Instantiatable objects
    public Rigidbody cookedFish;

    // Start is called before the first frame update
    void Start()
    {
        furnaceLight.SetActive(false);
        cookingFish.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StopCooking()
    {
        furnaceLight.SetActive(false);
        cookingFish.SetActive(false);
        isCooking = false;
        cookingFish.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        StopCoroutine(startCooking());
        StopCoroutine(startBurning());
    }

    public IEnumerator startCooking()
    {
        furnaceLight.SetActive(true);
        cookingFish.SetActive(true);
        float random = Random.Range(10f,21f);
        yield return new WaitForSeconds(random);
        cookingFish.GetComponent<Renderer>().material.color = new Color32(255, 152, 0, 255);
        hasCooked = true;
        StartCoroutine(startBurning());
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
        }
    }

    public void PrepareCookedFish()
    {
        Debug.Log("Preparing cooked fish");
        Rigidbody clone;
        clone = Instantiate(cookedFish, fishOut.position, Quaternion.identity);
        StopCooking();
    }
}

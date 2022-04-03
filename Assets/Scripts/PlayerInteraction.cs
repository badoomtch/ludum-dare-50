using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance;
    public TextMeshProUGUI helpText;

    public int caughtFish;
    public TextMeshProUGUI caughtFishText;

    public int woodCollected;
    public TextMeshProUGUI woodCollectedText;

    public GameObject furnace;
    public GameObject hitParticle;

    // Start is called before the first frame update
    void Start()
    {
        helpText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        woodCollectedText.text = woodCollected.ToString();
        caughtFishText.text = caughtFish.ToString();


        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (hit.transform.CompareTag("Fish"))
            {
                helpText.text = "Click to pick up";
                if (Input.GetMouseButtonDown(0))
                {   
                    caughtFish++;
                    Destroy(hit.transform.gameObject);
                }
            }
            else if (hit.transform.CompareTag("Wood"))
            {
                helpText.text = "Click to pick up";
                if (Input.GetMouseButtonDown(0))
                {   
                    woodCollected++;
                    Destroy(hit.transform.gameObject);
                }
            }
            else if (hit.transform.CompareTag("FryingPan"))
            {
                if(furnace.GetComponent<cookingScript>().isCooking && caughtFish > 0)
                {
                    helpText.text = "You can only cook one fish at a time";
                }
                else if(caughtFish > 0 && !furnace.GetComponent<cookingScript>().isCooking && furnace.GetComponent<cookingScript>().fuelValue > 0)
                {
                    helpText.text = "Cook fish";
                    if (Input.GetMouseButtonDown(0))
                    {
                        furnace.GetComponent<cookingScript>().isCooking = true;
                        StartCoroutine(furnace.GetComponent<cookingScript>().startCooking());
                        caughtFish--;
                    }
                }
                else if(caughtFish > 0 && !furnace.GetComponent<cookingScript>().isCooking && furnace.GetComponent<cookingScript>().fuelValue <= 0)
                {
                    helpText.text = "You have no fuel";
                }
                else
                {
                    helpText.text = "You have no fish to cook";
                }
                
            }
            else if (hit.transform.CompareTag("cookingFish"))
            {
                /* if (furnace.GetComponent<cookingScript>().hasCooked == false && furnace.GetComponent<cookingScript>().hasBurned == false)
                {
                    helpText.text = "Pick up raw fish";
                    if (Input.GetMouseButtonDown(0))
                    {   
                        caughtFish++;
                        furnace.GetComponent<cookingScript>().StopCooking();
                    }
                } */
                if(furnace.GetComponent<cookingScript>().hasCooked == true)
                {
                    helpText.text = "Prepare cooked fish";
                    if (Input.GetMouseButtonDown(0))
                    {
                        furnace.GetComponent<cookingScript>().PrepareCookedFish();
                    }
                }
                else if(furnace.GetComponent<cookingScript>().hasBurned == true)
                {
                    helpText.text = "Dispose of burned fish";
                    if (Input.GetMouseButtonDown(0))
                    {
                        furnace.GetComponent<cookingScript>().StopCooking();
                    }
                }
            }
            else if (hit.transform.CompareTag("cookedFish"))
            {
                helpText.text = "Click to eat fish";
                if (Input.GetMouseButtonDown(0))
                {   
                    this.GetComponentInParent<playerStats>().eatFood(70f);
                    Destroy(hit.transform.gameObject);
                }
            }
            else if (hit.transform.CompareTag("Furnace"))
            {
                if (woodCollected > 0)
                {
                    helpText.text = "Click to add wood fuel";
                    if (Input.GetMouseButtonDown(0))
                    {   
                        woodCollected--;
                        furnace.GetComponent<cookingScript>().fuelValue += 30f;
                        StartCoroutine(furnace.GetComponent<cookingScript>().StartFurnance());
                        StartCoroutine(this.GetComponentInParent<playerStats>().warmthUp());
                    }
                }
                else
                {
                    helpText.text = "You need wood for fuel";
                }
            }
            else if (hit.transform.CompareTag("hitPoint"))
            {
                helpText.text = "Click to chop wood";
                if (Input.GetMouseButtonDown(0))
                {   
                    hit.transform.GetComponentInParent<ChopWood>().NewHitPoint();
                    Debug.Log("Wood hit " + hit.transform.GetComponentInParent<ChopWood>().amountOfHits);
                    GameObject clone;
                    clone = Instantiate(hitParticle, hit.transform.position, Quaternion.identity);
                    Destroy(clone, 1f);
                }
            }
            else
            {
                helpText.text = "";
            }
        }
    }

    
}

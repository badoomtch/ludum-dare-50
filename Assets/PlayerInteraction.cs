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
        Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance);

        
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
        else if (hit.transform.CompareTag("Furnace"))
        {
            if(caughtFish > 0 && !furnace.GetComponent<cookingScript>().isCooking)
            {
                helpText.text = "Cook fish";
                if (Input.GetMouseButtonDown(0))
                {
                    furnace.GetComponent<cookingScript>().isCooking = true;
                    StartCoroutine(furnace.GetComponent<cookingScript>().startCooking());
                    caughtFish--;
                }
            }
            else if(furnace.GetComponent<cookingScript>().isCooking)
            {
                helpText.text = "You can only cook one fish at a time";
            }
            else
            {
                helpText.text = "You have no fish to cook";
            }
            
        }
        else if (hit.transform.CompareTag("cookingFish"))
        {
            if (furnace.GetComponent<cookingScript>().hasCooked == false)
            {
                helpText.text = "Pick up raw fish";
                if (Input.GetMouseButtonDown(0))
                {   
                    caughtFish++;
                    furnace.GetComponent<cookingScript>().StopCooking();
                }
            }
            else if(furnace.GetComponent<cookingScript>().hasCooked == true)
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
        else
        {
            helpText.text = "";
        }
    }

    
}

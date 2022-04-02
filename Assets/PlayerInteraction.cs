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
        Debug.DrawRay(transform.position, transform.forward, Color.green, interactionDistance, true);
        Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance);

        
        if (hit.transform.CompareTag("Fish"))
        {
            helpText.text = "Click to pick up";
            if (Input.GetMouseButtonDown(0))
            {   
                Debug.Log("You hit fish");
                caughtFish++;
                Destroy(hit.transform.gameObject);
            }
        }
        else if (hit.transform.CompareTag("Wood"))
        {
            helpText.text = "Click to pick up";
            if (Input.GetMouseButtonDown(0))
            {   
                Debug.Log("You hit wood");
                woodCollected++;
                Destroy(hit.transform.gameObject);
            }
        }
        else if (hit.transform.CompareTag("Furnace"))
        {
            helpText.text = "Click to pick up";
            if (Input.GetMouseButtonDown(0))
            {   
                Debug.Log("You hit wood");
                woodCollected++;
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            helpText.text = "";
        }
    }

    
}

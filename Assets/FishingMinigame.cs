using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FishingMinigame : MonoBehaviour
{
	/// <summary>
	/// This is the main fishing minigame code. It controls the states of fishing,
	/// then when in the minigame state it controls the movement of the catching
	/// bar and selects which fish we are catching.
	/// </summary>
	
	//These bools are just for keeping track of the state of the minigame we are in
	private bool lineCast = false;
    private bool nibble = false;
    public bool reelingFish = false;

    public Transform caughtFishTransform;
    public GameObject caughtFish;

	public GameObject fishingParticle;

    private Fish currentFishOnLine; //Reference to the current fish we are catching (Fish class is in Fish.cs)

    //These are references for the gameobjects used in the UI
    [Header("Setup References")]
    //The catching bar is the green bar that you put ontop of the fish to catch it
    [SerializeField] public GameObject catchingbar;
    public Vector3 catchingBarLoc;
    public Rigidbody2D catchingBarRB;
    
    //This is the fish on the UI that you are chasing to catch
    [SerializeField] public GameObject fishBar;
    public FishingMinigame_FishTrigger fishTrigger; //Reference to this script on the fish
    public bool inTrigger = false; //Whether or not the fish is inside the "catchingbar"
    
    public float catchPercentage = 0f; //0-100 how much you have caught the fish
    [SerializeField] public Slider catchProgressBar; //The bar on the right that shows how much you have caught

    [SerializeField] public GameObject thoughtBubbles;
    [SerializeField] public GameObject minigameCanvas;

    [Header("Settings")]
    public KeyCode fishingKey = KeyCode.F; //Key used to play
    [SerializeField] public float catchMultiplier = 10f; //Higher means catch fish faster x
    [SerializeField] public float catchingForce; //How much force to push the catchingbar up by
    
    private void Start() {
	    catchingBarRB = catchingbar.GetComponent<Rigidbody2D>(); //Get reference to the Rigidbody on the catchingbar
	    catchingBarLoc = catchingbar.GetComponent<RectTransform>().localPosition; //Use this to reset the catchingbars position to the bottom of the "water"
    }

    private void Update() {
	    if (!reelingFish) { //If we arent currently in the reeling stage
		    if (Input.GetKeyDown(fishingKey) && !lineCast) { //This is if we are doing nothing and are ready to cast a line
			    CastLine();
		    }else if (Input.GetKeyDown(fishingKey) && lineCast && !nibble) { //This is if the line has cast and we reel in before we get a nibble
			    StopAllCoroutines(); //Stops the WaitForNibble timer
			    lineCast = false; //Resets the line being cast
			    
			    //Resets the thought bubbles
			    thoughtBubbles.SetActive(false);
			    
		    }else if (Input.GetKeyDown(fishingKey) && lineCast && nibble) { //This is if we reel in while there is a nibble
			    StopAllCoroutines(); //Stops the LineBreak timer
			    StartReeling();
		    }
	    } else { //This is when we are in the stage where we are fighitng for the fish
		    if (Input.GetKey(fishingKey)) { //If we press space
			    catchingBarRB.AddForce(Vector2.up * catchingForce * Time.deltaTime, ForceMode2D.Force); //Add force to lift the bar
		    }
	    }

	    //If the fish is in our trigger box
	    if (inTrigger && reelingFish) {
		    catchPercentage += catchMultiplier * Time.deltaTime;
	    } else {
		    catchPercentage -= catchMultiplier * Time.deltaTime;
	    }
	    
	    //Changes fish from silhoutte to colour over time
	    var fishColor = Color.Lerp(Color.black, Color.white, Map(0, 100, 0, 1, catchPercentage));
	    fishBar.GetComponent<Image>().color = fishColor;
	    
	    //Clamps our percentage between 0 and 100
	    catchPercentage = Mathf.Clamp(catchPercentage, 0, 100);
	    catchProgressBar.value = catchPercentage;
	    if (catchPercentage >= 100) { //Fish is caught if percentage is full
		    catchPercentage = 0;
		    FishCaught();
	    }
    }
    
    //Called to cast our line
    private void CastLine() {
	    lineCast = true;
	    thoughtBubbles.SetActive(true);
	    StartCoroutine(WaitForNibble(10));
    }
    
    //Wait a random time to get a nibble
    private IEnumerator WaitForNibble(float maxWaitTime) {
	    yield return new WaitForSeconds(Random.Range(maxWaitTime * 0.25f, maxWaitTime)); //Wait between 25% of maxWaitTime and the maxWaitTime
	    fishingParticle.SetActive(true); //Show the alert thoughtbubble
	    nibble = true; 
	    StartCoroutine(LineBreak(2)); //If we dont respond in 2 seconds break the line
    }
    
    //Used to start the minigame
    private void StartReeling() {
	    reelingFish = true;
	    
	    nibble = false;
	    lineCast = false;
	    
	    //Set up the fish we are catching
	    currentFishOnLine = FishManager.GetRandomFishWeighted();
	    
	    minigameCanvas.SetActive(true);
    }
    
    //This breaks the line if we are waiting for a response too long
    private IEnumerator LineBreak(float lineBreakTime) {
	    yield return new WaitForSeconds(lineBreakTime);
	    Debug.Log("Line Broke!");
	    
	    //Disable thought bubbles
		fishingParticle.SetActive(false);
	    thoughtBubbles.SetActive(false);
	    
	    lineCast = false;
	    nibble = false;
    }

    //Called from the FishingMinigame_FishTrigger script
    public void FishInBar() {
	    inTrigger = true;
    }
    
	//Called from the FishingMinigame_FishTrigger script
    public void FishOutOfBar() {
	    inTrigger = false;
    }

    //Called when the catchpercentage hits 100
    public void FishCaught() {
	    Debug.Log("FishCaught");
		GameObject clone;
        clone = Instantiate(caughtFish, caughtFishTransform.position, Quaternion.identity);
	    reelingFish = false; //No longer reeling in a fish
	    //Reset the thought bubbles
	    thoughtBubbles.SetActive(false);
	    minigameCanvas.SetActive(false); //Disable the fishing canvas
	    catchingbar.transform.localPosition = catchingBarLoc; //Reset the catching bars position
    }
    

    //Classic mapping script x
    private float Map(float a, float b, float c, float d, float x) {
	    return (x - a) / (b - a) * (d - c) + c;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopWood : MonoBehaviour
{
    public Rigidbody log;

    public GameObject[] hitPoints;
    public GameObject player;

    public int amountOfHits;
    public int treeHealth;
    public int currenPointIndex = 999;

    public Transform logTransform;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject hitPoint in hitPoints)
        {
            hitPoint.SetActive(false);
        }
        NewHitPoint();
        treeHealth = Random.Range(4,7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewHitPoint()
    {
        int activeHitPoint = Random.Range(0,(hitPoints.Length - 1));
        if(currenPointIndex != activeHitPoint)
        {
            amountOfHits++;
            player.GetComponent<playerStats>().hungerLevel -= 2f;
            foreach (GameObject hitPoint in hitPoints)
            {
                hitPoint.SetActive(false);
            }
            currenPointIndex = activeHitPoint;
            hitPoints[activeHitPoint].SetActive(true);
            if (amountOfHits == treeHealth)
            {
                Destroy(gameObject);
                Rigidbody clone;
                clone = Instantiate(log, logTransform.position, Quaternion.identity);
                clone = Instantiate(log, logTransform.position, Quaternion.identity);
            }
        }
        else
        {
            NewHitPoint();
        }
    }
}

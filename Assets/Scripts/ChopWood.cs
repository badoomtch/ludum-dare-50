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

    public Transform logTransform;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject hitPoint in hitPoints)
        {
            hitPoint.SetActive(false);
        }
        NewHitPoint();
    }

    // Update is called once per frame
    void Update()
    {
        treeHealth = Random.Range(4,7);
    }

    public void NewHitPoint()
    {
        amountOfHits++;
        player.GetComponent<playerStats>().hungerLevel -= 2f;
        foreach (GameObject hitPoint in hitPoints)
        {
            hitPoint.SetActive(false);
        }
        int activeHitPoint = Random.Range(0,(hitPoints.Length - 1));
        hitPoints[activeHitPoint].SetActive(true);

        if (amountOfHits == treeHealth)
        {
            Destroy(gameObject);
            Rigidbody clone;
            clone = Instantiate(log, logTransform.position, Quaternion.identity);
        }
    }
}

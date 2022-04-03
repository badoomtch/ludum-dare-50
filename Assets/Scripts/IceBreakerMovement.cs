using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreakerMovement : MonoBehaviour
{
    public float boatSpeed;
    public Transform endPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        float step =  boatSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, endPosition.position) < 0.001f)
        {
            //boatSpeed = 0;
        }
    }
}

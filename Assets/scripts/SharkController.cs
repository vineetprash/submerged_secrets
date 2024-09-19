using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour

{

    public GameObject fish;
    public float SharkSmoothSpeed = .125f;

    private int start = 0;
    private Vector3 desiredPos;
    private Vector3 displacement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown("space"))
        {
            start = 1;
        }

        if (start == 1)
        {
        desiredPos = fish.transform.position;
        
        //transform.position = Vector3.Lerp(transform.position, desiredPos, SharkSmoothSpeed);
        displacement = desiredPos - transform.position;
        transform.position += displacement * SharkSmoothSpeed * Time.deltaTime; 
        }

    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject == fish)
        {
            print("GAME OVER");
            SharkSmoothSpeed = 0;
        }
    }
}

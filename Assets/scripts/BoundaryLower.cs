using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryLower : MonoBehaviour

{

    public Transform fish;
    public float xOffset = 0;
    public float yOffset = -20;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(xOffset,yOffset,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(fish.position.x + xOffset,yOffset,0);
    }
}

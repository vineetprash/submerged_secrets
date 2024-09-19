using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryUpper : MonoBehaviour
{

    public Transform fish;
    public float yOffset = -25;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,yOffset,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(fish.position.x,yOffset,0);
    }
}

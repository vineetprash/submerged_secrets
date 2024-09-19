using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,0,-10);
    public float smoothSpeed = 0.125f;
    public GameObject Character;

    private Vector3 displacement;
    void LateUpdate()
    {
        Vector3 desiredPos = Character.transform.position + offset;
        displacement = desiredPos - transform.position;
        transform.position += new Vector3(displacement.x * smoothSpeed * Time.deltaTime,displacement.y * smoothSpeed * Time.deltaTime,0); 
  
    }
}

// scrolls a quad object
using UnityEngine;
using System.Collections;

public class Done_BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileWidth;
	public Transform fish;
	
	private int H,W;
	private BirdBehaviour fishScript;
	private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;	
		fishScript = fish.GetComponent<BirdBehaviour>();
		H = fishScript.H;
		W = fishScript.W;
		
	}

	void Update ()
	{
		// float x_comp = -fishScript.x;
		// float fishSpeed = fishScript.speed;


		if (fish.position.x >= transform.position.x + tileWidth)
		{
			transform.position += new Vector3(tileWidth,0,0);
		}
		// float newPosition = Mathf.Repeat(x_comp*Time.deltaTime*fishSpeed*scrollSpeed, tileWidth);
		// transform.position = startPosition + Vector3.left * newPosition;


	}
}
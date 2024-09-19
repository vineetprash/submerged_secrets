using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float offset = 20f;
    public List<GameObject> Obstacles;
    public GameObject boosterObject;
    public Transform fish;
    public float max_sum_discrete = 0;
    public float max_sum_continuos = 0;
    public float maxdist_btw_obs = 45;
    public float deletion_dist = 75;
    public float yPos = 0;
    public float Screen_MaxY = 16.5f;
    public float time_btw_boosters = 10;

    private Vector3 lastPos;
    private List<GameObject> Clones = new List<GameObject>();
    private List<GameObject> boosterClones = new List<GameObject>();
    private float booster_yPos;
    private Vector3 apparentPos;
    private float time_elapsed;
    private int start = 0;
    
    void Start()
    {
        lastPos = fish.position + new Vector3(offset,yPos,0);
        spawn_obs(lastPos);
    }

    void LateUpdate()
    {   
        if (Input.GetKeyDown("space")){
            start = 1;
        }

        if (start == 1)
        {
        apparentPos = new Vector3(fish.position.x + offset,yPos,0);
        max_sum_continuos = Mathf.Max(max_sum_continuos,apparentPos.x);
        // Positioning the empty object at new location as long as its more than the max recorded x position
        transform.position = new Vector3(Mathf.Max(max_sum_discrete,apparentPos.x),yPos,0);
        

        // FOR OBSTACLES
        // Code for spawning new obstaces at regular intervals of distance along x, as long as position is higher than max recorded x
        if (Vector3.Distance(lastPos,apparentPos) >= maxdist_btw_obs  &&  apparentPos.x > max_sum_discrete)
        {
            spawn_obs(apparentPos);
            lastPos = apparentPos;
            max_sum_discrete = apparentPos.x;
        }
        // Code for deleting old obstacles
        if (Clones.Count > 0)
        {
            if (Vector3.Distance(Clones[0].transform.position, apparentPos) >= deletion_dist)
            {
                Destroy(Clones[0]);
                Clones.RemoveAt(0);
            }
        }


        // FOR BOOSTERS
        time_elapsed += Time.deltaTime;
        if (time_elapsed >= time_btw_boosters) 
        {
            time_elapsed = 0;
            spawn_boost();
        }
        if (boosterClones.Count > 0)
        {
            if (Vector3.Distance(boosterClones[0].transform.position, apparentPos) >= deletion_dist)
            {

                Destroy(boosterClones[0]);
                boosterClones.RemoveAt(0);
            }
        }
        }
        
       
        
    }
    
    void spawn_boost()
    {
        booster_yPos = Random.Range(-Screen_MaxY, Screen_MaxY);    
        GameObject temp = Instantiate(boosterObject, new Vector3(apparentPos.x + Random.Range(5,30),booster_yPos,0),Quaternion.identity);
        boosterClones.Add(temp);
        print("Spawned new booster" + boosterClones[0]);
    }

   

    void spawn_obs(Vector3 spawn_point)
    {
        GameObject temp_object = Obstacles[Random.Range(0,Obstacles.Count)];
        GameObject clone = Instantiate(temp_object,spawn_point,Quaternion.identity);
        Clones.Add(clone);
    }

    public void max_sum_discrete_calculator()
    {
        max_sum_discrete = 0;

    }
    public void del_from_boosterClones()
    {
        boosterClones.RemoveAt(0);
    }
}

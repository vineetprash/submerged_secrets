using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class BirdBehaviour : MonoBehaviour
{
    //public Vector2 mouse = MouseEventBase.mouseDelta;
    
    // Start is called before the first frame update
    public Vector3 init_mousePos;
    public float speed;
    public float max_speed;
    public float reduced_speed;
    public float penalty_duration = 5;
    public float default_camsize = 10;
    public float reduced_camsize = 8;
    public int H, W;
    public Camera mainCam;
    public GameObject booster_refference;
    public GameObject Volume;
    public float x,y,theta;
    public Text HighScoreText;
    public Text RestartInfoText;
    public Text BoosterCollectedText;
    
    private string highscore;
    private string filePath;
    private int boosterScore = 0;
    private int running = 1;
    private float time_elapsed;
    private int slowed = 0;
    private int start = 0;
    private float speed_ref;
    private ObstacleSpawner script1Component;
    void NewLevel() 
    {
        
        int n = UnityEngine.Random.Range(0,8);
        Vector2[] boostPos = new Vector2[n];
        for(int i = 0; i<n ; i++) {
            boostPos[i] = new Vector2(UnityEngine.Random.Range(0,W), UnityEngine.Random.Range(0,H));
        }
        // Spawn boosters/coins

    }
    void Start() 
    { 
        Cursor.visible = false;
        H = Screen.height;
        W = Screen.width;
        init_mousePos = new Vector3(W/2,H/2,0);
        speed_ref = speed;
        print(H);
        print(W);
        // Accessing fun del_from_boosterClones from Script1
        // First, find the GameObject with Script1 attached to it
        GameObject script1Object = GameObject.Find("ObstcleSpawner");

        // Next, get the Script1 component attached to the GameObject
        script1Component = script1Object.GetComponent<ObstacleSpawner>();

        // Now you can call the public function from Script1  



        // Extracting previous highscore from txt file
        filePath = Path.Combine(Application.dataPath, "scripts/Info.txt");
        using(StreamReader inp_stm = new StreamReader(filePath))
        {
            highscore = inp_stm.ReadLine( );
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            start = 1;
            HighScoreText.text = "";
            RestartInfoText.text = "";
            Cursor.visible = true;
        }

        if (start == 1 && running == 1){
        Vector3 mousePos = Input.mousePosition;
        // x is net mouse displacement along x
        // Clamp that displacement to Screen.Width / 2;
        // float x = Mathf.Clamp(-W/2,W/2,mousePos.x - init_mousePos.x);
        // float y = Mathf.Clamp(-H/2,H/2,mousePos.y - init_mousePos.y);

        x = mousePos.x - init_mousePos.x;
        y = mousePos.y - init_mousePos.y;
        theta = Mathf.Atan2(y, x) * Mathf.Rad2Deg;


        // Changing world coords to screen coords
        float[] fishPos = {mainCam.WorldToScreenPoint(transform.position).x, mainCam.WorldToScreenPoint(transform.position).y}; 

        // Make the fish object look towards the cursor position
        if (theta < 90 || theta > -90)
        {   
            
            transform.rotation = Quaternion.AngleAxis(theta,Vector3.forward);
        } else
        {
            
            transform.rotation = Quaternion.AngleAxis(theta,Vector3.forward);  
        }

        
        // Changing position
        transform.position += new Vector3(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);

        // Resetting the penalty for hitting obstacles after n seconds
        if (slowed == 1)
        {
            if (time_elapsed < penalty_duration)
            {
                time_elapsed += Time.deltaTime;
                mainCam.orthographicSize += Time.deltaTime * ((default_camsize-reduced_camsize)/penalty_duration);
            }
            else
            {
                time_elapsed = 0;
                slowed = 0;
                speed = speed_ref;
                
                print("FISH SPEED RESET");
            }
        }
        }
        if (start == 1 && Input.GetKeyDown("space") && boosterScore >= 1 ){
            // Increase speed for 5 seconds and decrease booster
            BoosterCollectedText.text = "Speed Boost x" + (--boosterScore).ToString();
            speed += (float) 0.03;
            
            
       }


       if (running == 0 && Input.GetKeyDown("space")){
            // Reset Level
            SceneManager.LoadScene("SampleScene");
       }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        // Using a function from exteernal ObstacleSpawner.cs file
        print("Fish hit" + col);
        if (col.gameObject.tag == "Obstacle")
        {
            print("FISH SLOWED");
            slowed = 1;
            speed = reduced_speed;
            mainCam.orthographicSize = reduced_camsize;
          
        }
        if (col.gameObject.tag == "Booster")
        {   
            print("Fish hit Booster");
            Destroy(col.gameObject);
            script1Component.del_from_boosterClones();
            BoosterCollectedText.text = "Speed Boost x" + (++boosterScore).ToString();
        }

        if (col.gameObject.tag == "Shark")
        {   
            print("Yea, fish hit shark");
            if (mainCam.orthographicSize > 0)
            {mainCam.orthographicSize -= Time.deltaTime;}
            speed = 0;
            running = 0;
            start = 0;
            int curr_score = ((int)script1Component.max_sum_continuos/10);
            int prev_highscsore = System.Convert.ToInt32(highscore);
            if (curr_score > prev_highscsore){
            HighScoreText.text = "New Highscore : " + curr_score.ToString();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(curr_score.ToString());
            }
            }
            RestartInfoText.text = "Press Space to reset";
        }
    }

    
}

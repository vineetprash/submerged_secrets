using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public ObstacleSpawner ObsScript; // Reference to the script containing the variable
    public Text scoreText; // Reference to the Text component of the UI element

    void Update()
    {
        // Update the UI text with the value of the variable from the GameManager script
        scoreText.text = "Score: " + ((int)(ObsScript.max_sum_continuos/10)).ToString();
    }
}


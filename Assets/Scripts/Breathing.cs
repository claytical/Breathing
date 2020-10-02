using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breathing : MonoBehaviour
{

    public Slider slider; 
    public Text displayText;
    private float currentValue;
    public Button button;
    public bool exhaling = false;
    public PlayerInput player;
    public Text scoreText;
    private int score;
    public Text timeText;

     public float CurrentValue {
        get {
            return currentValue;
        }
        set {
            currentValue = value;
            slider.value = currentValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentValue = 0f;
        scoreText.text = "0";
        score = 0;
        timeText.text = "0:10";
    }

    // Update is called once per frame
    void Update()
    {
        if(!exhaling)
        {
            timeText.text = "0:" + ((10 - (int)(player.getBreathsCaptured()%10))).ToString();
            //((int) System.Math.Ceiling((10-player.getBreathsCaptured()))).ToString();
        }
        if(exhaling)
        {
            timeText.text = "0:" + ((0+(int)(player.getBreathsCaptured()%10))).ToString();
        }
        CurrentValue = player.getBreathsCaptured()/5f;
        if(CurrentValue > .999 && !exhaling)
        {
            exhaling = true;
            displayText.text = "Exhale";
        }

        if(CurrentValue < .001 && exhaling)
        {
            exhaling = false;
            displayText.text = "Breathe In";
            score++;
            scoreText.text = score.ToString();
        }
    }
}

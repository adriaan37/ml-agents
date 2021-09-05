using UnityEngine;
using UnityEngine.UI;


public class GameTimer : MonoBehaviour
{

    [SerializeField]
    private float[] mileStones;
    public Pipe pipe;
    public Bird bird;
    public Text currentTimeText;

    bool stopwatchActive = false;
    float currentTime;
    private float lastTimeStamp;
    private int i=0;
    private void Start() 
    {
        currentTime = 0f;  
        stopwatchActive = true;     
    }

    void Update()
    {
        if(stopwatchActive == true)
        {        
            currentTime = currentTime + Time.deltaTime;
            int score =(int)currentTime;
                  
            // Milestone that can be set in the inspector if a Milestone is reached a large reward is given         
                           
                if(score == mileStones[i])
                    {   Debug.Log($"{score} reached");    
                        bird.AddReward(2.0f);
                        PlusReward();
                        i++;
                    }
                    
        }
    }
    
    // Reset pipe speed current time and milestone number 
    public void Restart()
    {   
        pipe.speed = 4f;
        currentTime = 0;
        i=0;
    }
// Increase pipe movement speed to give the illusion that game is speeding up and increases the reward size
    public void PlusReward()
    {   
        pipe.speed += 0.2f;
        Debug.Log($"{pipe.speed} speed");
        bird.plusReward += 0.2f;
    }
}



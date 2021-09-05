using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;


public class Bird : Agent
{
    [Header("References")]
    [SerializeField] private Rigidbody rb =null;
    [SerializeField] private PipeHandler pipehandler= null;
    [SerializeField] private Transform bodyTransform = null;
    [SerializeField] private GameTimer timer;
    [SerializeField] private EnemyHandler enemyhandler = null;
   

    [Header("Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxVelocityMagnitude = 5f;

    private Vector3 startingPos;
    public float plusReward = 0.1f;
    public float minusReward = -1.0f;

    public override void Initialize()
    {
        startingPos = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startingPos;
        rb.velocity = Vector3.zero;

        timer.Restart();
        pipehandler.ResetPipes();
        enemyhandler.ResetEnemies();
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(plusReward);

        if(Mathf.FloorToInt(actions.DiscreteActions[0])!=1f){return;}
        Jump();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        rb.velocity  = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {  
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
        
    }

   private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            AddReward(-1.1f);
            EndEpisode();
        }
        if(other.tag == "Pipe")
        {
            AddReward(minusReward);
            EndEpisode();
        }
        
    }

    private void Update() {
        bodyTransform.rotation = Quaternion.LookRotation(rb.velocity + new Vector3(15f,0f,0f),transform.up);
    }





}


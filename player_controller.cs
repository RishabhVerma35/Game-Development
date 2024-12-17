using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class player_controller : Agent
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField]private Transform startPosition;
    

    public override void OnEpisodeBegin()
    {
        transform.position = startPosition.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.position);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousAction = actionsOut.ContinuousActions;
        continousAction[0] = Input.GetAxisRaw("Horizontal");
        continousAction[1] = Input.GetAxisRaw("Vertical");
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float rotateAction = actions.ContinuousActions[0]; // Rotation action
    float moveAction = actions.ContinuousActions[1];   // Forward movement action

    // Apply rotation based on the continuous action
    transform.Rotate(Vector3.up * rotateAction * rotationSpeed * Time.fixedDeltaTime);

    // Apply forward movement based on the continuous action
    rb.velocity = transform.forward * moveAction * movementSpeed;

    }


    void OnCollisionEnter(Collision collision)
{
     if (collision.gameObject.CompareTag("tag"))
    {
        SetReward(-1f);
        EndEpisode();
        
    }
    if(collision.gameObject.CompareTag("goal"))
    {
        SetReward(1f);
        EndEpisode();
       GameObject prefab_Obj = Resources.Load<GameObject>("Gas");
       Instantiate(prefab_Obj,transform.position,transform.rotation);
    }
}
}

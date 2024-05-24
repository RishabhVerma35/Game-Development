using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.PlayerLoop;
public class my_Player : Agent
{
    private Rigidbody rb;   
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform Target_Pos;
    public Transform[] girlPositions;
    float previousDistance;
    public GameObject[] checkpoints;
    public Transform startPos;
    int count = 0;
    int girlPos = 0;
    public float maxTime_Reward = 8;// seconds
    float time_Counter = 0;
    float initialDistanceToGoal;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        time_Counter-=Time.deltaTime;
    }
     public override void OnEpisodeBegin()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Transform parentTransform = Target_Pos.parent;
        transform.position = startPos.position;
        
      transform.rotation = startPos.rotation;
previousDistance = Vector3.Distance(transform.localPosition, Target_Pos.localPosition);
int random_Position = Random.Range(0, girlPositions.Length);
time_Counter = maxTime_Reward;
initialDistanceToGoal = Vector3.Distance(transform.position, Target_Pos.position);
for(int i =0;i<checkpoints.Length;i++)
{
    checkpoints[i].SetActive(true);
}
    }
    public override void CollectObservations(VectorSensor sensor)
    {
       sensor.AddObservation(Target_Pos.localPosition);
        //self position
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(Vector3.Distance(transform.localPosition,Target_Pos.position));
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
       float moveAction = actions.ContinuousActions[0];   // Forward movement action
        float rotateAction = actions.ContinuousActions[1];
    // Apply rotation based on the continuous action
    transform.Rotate(Vector3.up * rotateAction * rotationSpeed );

    // Apply forward movement based on the continuous action
    rb.velocity = transform.forward * moveAction * movementSpeed;

    if(count%30 ==0)
    {
        count++;
        if(girlPos<girlPositions.Length) girlPos++;

        Target_Pos.position = girlPositions[girlPos].position;
    }

     
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
         ActionSegment<float> continousAction = actionsOut.ContinuousActions;
         continousAction[1] = Input.GetAxisRaw("Horizontal");
    continousAction[0] = Input.GetAxisRaw("Vertical");
    }
   void OnTriggerEnter(Collider other)
    {
 
 
     if (other.gameObject.CompareTag("wall"))
    {
   
        EndEpisode();
    }
    else if (other.gameObject.CompareTag("goal"))
    {
        AddReward(15f);
        AddReward(time_Counter * 0.1f);
        count+=1;
     

          EndEpisode();
    }
    if(other.gameObject.CompareTag("check"))
    {
          other.gameObject.SetActive(false);
        AddReward(1f);
       

    }
}
}

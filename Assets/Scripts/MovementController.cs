using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class MovementController : MonoBehaviour
{
    public Transform duckModelTransform;
    protected NavMeshAgent agent;
    public MovementModeEnum movementMode;
    // later you can make null check in methods
    protected float lastVelocity = 0.0f;

    public float dashingSpeed = 5.0f;
    public float dashingAcceleration = 7.0f;
    public float swimmingSpeed = 3.0f;
    public float swimmingAcceleration = 3.0f;
    public float slowdownVelocity = 25.0f;
    public float velocity;
    public float acceleration;
    public float startDistance = 0.0f;
    //public float maximumSlowdownDistance = 200.0f;

    protected void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        movementMode = MovementModeEnum.Swimming;
        agent.speed = swimmingSpeed;
        agent.acceleration = swimmingAcceleration;
    }

    //private void Awake()
    //{
    //    movementMode = MovementModeEnum.Swimming;
    //    agent.speed = swimmingSpeed;
    //    agent.acceleration = swimmingAcceleration;
    //}
    protected void Update()
    {
        velocity = agent.velocity.magnitude;
        acceleration = agent.acceleration;
        //Debug.Log($"Current velocity: {agent.velocity.magnitude}");
        //switch (movementMode)
        //{
        //    case MovementModeEnum.Swimming:
        //    if (agent.remainingDistance <= startDistance/2) {
        //            agent.acceleration = swimmingAcceleration * (agent.remainingDistance - startDistance);
        //        }
        //        break;
        //    case MovementModeEnum.Dashing:
        //        if (agent.remainingDistance <= startDistance / 2)
        //        {
        //            agent.acceleration = dashingAcceleration * (agent.remainingDistance - startDistance);
        //        }
        //        break;
        //}
    }
    protected void MovePlayerToTarget(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.destination = hit.point;
            startDistance = agent.remainingDistance;
        }
    }
    protected void InitiateDash() 
    {
        Debug.Log("Initiated dash.");
        movementMode = MovementModeEnum.Dashing;
        agent.speed = dashingSpeed;
        agent.acceleration = dashingAcceleration;
        //StopAllCoroutines();
        //StartCoroutine(ChangeAltitudeCoroutine(3.0f));
    }
    protected void CheckForDashEnd() 
    {
        if (movementMode == MovementModeEnum.Dashing && agent.velocity.magnitude <= lastVelocity && agent.velocity.magnitude < slowdownVelocity)
        {
            Debug.Log("Stopping dash");
            movementMode = MovementModeEnum.Swimming;
            agent.speed = swimmingSpeed;
            agent.acceleration = swimmingAcceleration;
            //StopAllCoroutines();
            //StartCoroutine(ChangeAltitudeCoroutine(0.0f));
        }
        lastVelocity = agent.velocity.magnitude;
    }
    protected IEnumerator ChangeAltitudeCoroutine(float targetHeight)
    {
        float maxDistance = 10.0f;
        float ascentSpeed = maxDistance / 80;
        Transform modelTransform = duckModelTransform;
        while (true) {
            float _y = modelTransform.localPosition.y;
            print(_y);
            if (_y > targetHeight)
            {
                print("Descending");
                while (targetHeight - modelTransform.localPosition.y < -0.25f)
                {
                    modelTransform.localPosition = new Vector3(modelTransform.localPosition.x,
                    modelTransform.localPosition.y - ascentSpeed * 4, modelTransform.localPosition.z);
                    yield return new WaitForSeconds(0.025f);
                }
                break;
            }
            else
            {
                while (targetHeight - modelTransform.localPosition.y > 0.25f)
                {
                    print("Ascending");
                    modelTransform.localPosition = new Vector3(modelTransform.localPosition.x,
                    modelTransform.localPosition.y + ascentSpeed, modelTransform.localPosition.z);
                    yield return new WaitForSeconds(0.025f);
                }
                break;
            }
        }
    }
}
public enum MovementModeEnum { Swimming, Dashing };
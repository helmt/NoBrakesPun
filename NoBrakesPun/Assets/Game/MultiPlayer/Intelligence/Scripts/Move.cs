using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Move : MonoBehaviour
{

    public bool active = true;
    public GameObject graph;
    [HideInInspector] public Node[] nodes;
    
    public int current;
    public int objective;

    public float maxSpeed = 50f;
    public float minSpeed = 5f;
    private float speed = 50f;
    private float realSpeed;
    private float reachDistance = 2f;
    public float rotationSpeed = 8f;
    private float distance;

    private Vector3 currentPosition;
    private Vector3 objectivePosition;
    private System.Random rng = new System.Random();

    [Header("Sensors")]
    public float offset = 30f;
    public float sideOffset = 15f; 
    public float farSideOffset = 5f;
    public float sensorLength = 500f;
    public float sensorX = 4.5f;
    public float sensorZ = 18f;
    public float farSensorAngle = 40f;

    Vector3 GameObjectPosition(Component component)
    {
        return component.gameObject.transform.position;
    }

    Quaternion GameObjectRotation(Component component)
    {
        return component.gameObject.transform.rotation;
    }
    
    public void FindNextNode()
    {
        try
        {
            Node currentNode = nodes[current];
            objective = currentNode.neighbours[rng.Next(currentNode.neighbours.Count)];
            objectivePosition = nodes[objective].transform.position;
        }
        catch (Exception)
        {
            active = false;
        }
    }

    private void Start() => active = true;

    void FixedUpdate()
    {
        if (active)
        {
            // Move to objective
            currentPosition = transform.position;
            distance = Vector3.Distance(objectivePosition, currentPosition);

            // Speed
            speed = distance;
            if (speed < minSpeed)
                speed = minSpeed;
            else if (speed > maxSpeed)
                speed = maxSpeed;
            realSpeed = speed;

            // Sensors
            RaycastHit hit;
            Vector3 sensorStartPos = transform.position - transform.right * sensorX + transform.forward * sensorZ;

            // front sensor
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength) &&
                hit.transform.CompareTag("vehicle"))
            {
                realSpeed = Vector3.Distance(currentPosition, hit.point);
                realSpeed = realSpeed < offset ? 0 : realSpeed;
            }

            // right sensor
            sensorStartPos = transform.position + transform.forward * sensorZ;
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength) && hit.transform.CompareTag("vehicle"))
            {
                realSpeed = Math.Min(Vector3.Distance(currentPosition, hit.point), realSpeed);
                realSpeed = realSpeed < sideOffset ? 0 : realSpeed;
            }

            // left sensor
            sensorStartPos = transform.position - 2 * transform.right * sensorX + transform.forward * sensorZ;
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength) && hit.transform.CompareTag("vehicle"))
            {
                realSpeed = Math.Min(Vector3.Distance(currentPosition, hit.point), realSpeed);
                realSpeed = realSpeed < sideOffset ? 0 : realSpeed;
            }
            // far right sensor
            sensorStartPos = transform.position + transform.forward * sensorZ;
            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(farSensorAngle, transform.up) * transform.forward,
                    out hit, sensorLength) && hit.transform.CompareTag("vehicle"))
            {
                realSpeed = Math.Min(Vector3.Distance(currentPosition, hit.point), realSpeed);
                realSpeed = realSpeed < farSideOffset ? 0 : realSpeed;
            }

            // far left sensor
            sensorStartPos = transform.position - 2 * transform.right * sensorX + transform.forward * sensorZ;
            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-farSensorAngle, transform.up) * transform.forward,
                    out hit, sensorLength) && hit.transform.CompareTag("vehicle"))
            {
                realSpeed = Math.Min(Vector3.Distance(currentPosition, hit.point), realSpeed);
                realSpeed = realSpeed < farSideOffset ? 0 : realSpeed;
            }

            gameObject.transform.position = Vector3.MoveTowards(GameObjectPosition(this), objectivePosition, Time.deltaTime * realSpeed);
            Quaternion rotation = Quaternion.LookRotation(objectivePosition - GameObjectPosition(this));
            transform.rotation = Quaternion.Slerp(GameObjectRotation(this), rotation, Time.deltaTime * rotationSpeed);

            if (distance <= reachDistance)
            {
                current = objective;
                FindNextNode();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        active = false;
    }

    private void OnCollisionExit(Collision other)
    {
        active = true;
    }
}

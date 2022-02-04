using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private SpaceShipInput spaceShipInput;

    private int movementSpeed = 15;

    private void Awake()
    {
        spaceShipInput = GetComponent<SpaceShipInput>();
    }

    private void FixedUpdate()
    {
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 spaceShipToDestination = spaceShipInput.Destination - transform.position;

        if (spaceShipToDestination.magnitude < 0.3f)
            return;

        transform.position += spaceShipToDestination.normalized * movementSpeed * Time.fixedDeltaTime;
    }
}

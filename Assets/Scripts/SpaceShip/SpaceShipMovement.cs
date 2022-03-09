using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private Vector3 idlePosition = new Vector3(0f, -1f, 0f);

    private int movementSpeed = 35;

    private Vector3 fingerPositionOffset = new Vector3(0f, 1.5f,0f);

    private float movementThreshold = 1f;

    private void Update()
    {
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 destination;

        // When the player is not controlling the spaceship, we return it to its idle position
        if (SpaceShipInput.Instance.PositionInput == null)
            destination = idlePosition;

        else
            destination = (Vector3)SpaceShipInput.Instance.PositionInput + fingerPositionOffset;

        if (destination == transform.position)
            return;

        Vector3 spaceShipToDestination = destination - transform.position;

        if (spaceShipToDestination.magnitude < movementThreshold)
            transform.position = destination;

        else
            transform.position += spaceShipToDestination.normalized * movementSpeed * Time.deltaTime;
    }
}

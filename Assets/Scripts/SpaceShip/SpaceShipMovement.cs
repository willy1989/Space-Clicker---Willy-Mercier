using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private Vector3 idlePosition = new Vector3(0f, -3f, 0f);

    private int movementSpeed = 25;

    private Vector3 fingerPositionOffset = new Vector2(0f, 1.5f);

    private float movementLimitYPosition = -4f;

    private void FixedUpdate()
    {
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 spaceShipToDestination;

        if (SpaceShipInput.Instance.Destination == Vector3.zero)
        {
            spaceShipToDestination = idlePosition - transform.position;
        }

        else
        {
            spaceShipToDestination = (SpaceShipInput.Instance.Destination + fingerPositionOffset) - transform.position;
        }

        if (spaceShipToDestination.magnitude < 0.3f || SpaceShipInput.Instance.Destination.y <= movementLimitYPosition)
            return;

        transform.position += spaceShipToDestination.normalized * movementSpeed * Time.fixedDeltaTime;
    }
}

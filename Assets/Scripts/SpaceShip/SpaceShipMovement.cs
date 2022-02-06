using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private int movementSpeed = 25;

    private Vector3 fingerPositionOffset = new Vector2(0f, 1.5f);

    private float movementLimitYPosition = -4f;

    private void FixedUpdate()
    {
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 spaceShipToDestination = (SpaceShipInput.Instance.Destination + fingerPositionOffset) - transform.position;

        if (spaceShipToDestination.magnitude < 0.3f || SpaceShipInput.Instance.Destination.y <= movementLimitYPosition)
            return;

        transform.position += spaceShipToDestination.normalized * movementSpeed * Time.fixedDeltaTime;
    }
}

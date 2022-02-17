using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private Vector3 idlePosition = new Vector3(0f, -1f, 0f);

    private int movementSpeed = 25;

    private Vector3 fingerPositionOffset = new Vector3(0f, 1.5f,0f);

    private void FixedUpdate()
    {
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 spaceShipToDestination;

        if (SpaceShipInput.Instance.PositionInput == null)
            spaceShipToDestination = idlePosition - transform.position;

        else
            spaceShipToDestination = (Vector3)SpaceShipInput.Instance.PositionInput + fingerPositionOffset - transform.position;

        if (spaceShipToDestination.magnitude < 0.3f)
            return;

        transform.position += spaceShipToDestination.normalized * movementSpeed * Time.fixedDeltaTime;
    }
}

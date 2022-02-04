using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput : MonoBehaviour
{
    private Vector2 destination;

    public Vector2 Destination
    {
        get
        {
            return Destination;
        }
    }

    private void SetDestination()
    {
        if (Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        {
            destination = Camera.main.ScreenToWorldPoint(touch.position);
        }
    }
}

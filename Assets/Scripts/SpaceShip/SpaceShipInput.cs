using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipInput : MonoBehaviour
{
    private Vector3 destination;

    public Vector3 Destination
    {
        get
        {
            return destination;
        }
    }

    private void Update()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        if (Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(touch.position);
            destination = new Vector3(temp.x, temp.y, 0f);
        }
    }
}

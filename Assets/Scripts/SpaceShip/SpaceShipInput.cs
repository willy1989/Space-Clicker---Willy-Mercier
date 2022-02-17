using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceShipInput : Singleton<SpaceShipInput>
{
    public Vector2? PositionInput { get; private set; }

    private void Awake()
    {
        SetInstance();
    }

    private void Update()
    {
        PositionInput = RegisterDestinationInput();
    }

    private Vector2? RegisterDestinationInput()
    {
        if (Input.touchCount <= 0)
            return null;

        Touch touch = Input.GetTouch(0);

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) == true || touch.phase == TouchPhase.Ended)
            return null;

        else
        {
            return (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
        }
    }
}

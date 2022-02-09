using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipInput : Singleton<SpaceShipInput>
{
    private Vector3 destination;

    public Vector3 Destination
    {
        get
        {
            return destination;
        }
    }

    private bool canRegisterInput = true;

    [SerializeField] private Button openShopButton;

    [SerializeField] private Button closeShopButton;


    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        openShopButton.onClick.AddListener(ToggleInput);
        closeShopButton.onClick.AddListener(ToggleInput);
    }

    private void Update()
    {
        RegisterDestinationInput();
    }

    private void RegisterDestinationInput()
    {
        if (Input.touchCount <= 0 || canRegisterInput == false)
            return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(touch.position);
            destination = new Vector3(temp.x, temp.y, 0f);
        }

        if (touch.phase == TouchPhase.Ended)
            destination = Vector3.zero;
    }

    private void ToggleInput()
    {
        canRegisterInput = !canRegisterInput;
    }
}

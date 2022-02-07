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

    public Action DoubleTapEvent;

    private float doubleTapDelay = 0.25f;

    private bool tappedOnce = false;

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
        SetDestination();
        RegisterDoubleTap();
    }

    private void SetDestination()
    {
        if (Input.touchCount <= 0 || canRegisterInput == false)
            return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(touch.position);
            destination = new Vector3(temp.x, temp.y, 0f);
        }
    }

    private void RegisterDoubleTap()
    {
        if (Input.touchCount <= 0 || canRegisterInput == false)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (tappedOnce == true)
        {
            tappedOnce = false;

            Debug.Log("Double tap");

            if (DoubleTapEvent != null)
                DoubleTapEvent.Invoke();
        }
            
        else
        {
            StartCoroutine(doubleTapDelayCoroutine());
        }
            
    }

    private IEnumerator doubleTapDelayCoroutine()
    {
        tappedOnce = true;

        float timeLeft = doubleTapDelay;

        while(timeLeft >= 0f)
        {
            timeLeft -= Time.deltaTime;

            yield return null;
        }

        tappedOnce = false;
    }


    private void ToggleInput()
    {
        canRegisterInput = !canRegisterInput;
    }
}

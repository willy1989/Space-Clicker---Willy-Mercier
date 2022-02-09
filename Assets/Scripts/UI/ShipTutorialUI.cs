using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTutorialUI : MonoBehaviour
{
    private int tutorialDuration = 5;

    private void Start()
    {
        StartCoroutine(disableTutorial());
    }


    private IEnumerator disableTutorial()
    {
        yield return new WaitForSeconds(tutorialDuration);

        gameObject.SetActive(false);
    }
}

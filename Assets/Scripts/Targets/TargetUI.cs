using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Target))]
public class TargetUI : MonoBehaviour
{
    [SerializeField] private Text currentHealthText;

    private Target target;

    private void Awake()
    {
        target = GetComponent<Target>();
        target.UpdateHealthEvent += UpdateCurrentHealthText;
    }

    private void UpdateCurrentHealthText(int currentHealth)
    {
        currentHealthText.text = currentHealth.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipLevelUI : MonoBehaviour
{
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SpaceShipLevelManager.Instance.LevelIncreasedEvent += PlayAnimation;
    }

    private void PlayAnimation()
    {
        animator.SetTrigger(Constants.LevelUp_AnimationTrigger);
    }
    

}

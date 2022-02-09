using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    [SerializeField] private SoundPlayer soundPlayer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Target_Tag) == true)
        {
            soundPlayer.PlaySoundEffect();
            GameLoopManager.Instance.GameOver();
        }
            
    }
}

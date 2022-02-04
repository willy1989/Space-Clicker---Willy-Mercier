using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Target_Tag) == true)
        {

        }
            
    }
}

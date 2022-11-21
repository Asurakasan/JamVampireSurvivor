using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehavior : MonoBehaviour
{
    private GameObject _player;
    public int HouseCandy = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            if (collision.gameObject.GetComponent<PlayerController>().CurrentCandy > 0) {
                HouseCandy += collision.gameObject.GetComponent<PlayerController>().CurrentCandy;
                collision.gameObject.GetComponent<PlayerController>().CurrentCandy = 0;
            }


        }
    }

    
}

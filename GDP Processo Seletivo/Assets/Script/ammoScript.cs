using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovimento.ammo += playerMovimento.ammoMax;
            Destroy(gameObject);
        }

    }
}

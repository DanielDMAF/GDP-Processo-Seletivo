using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovimento.vida = playerMovimento.vidaMax;
            Destroy(gameObject);
        }

    }
}

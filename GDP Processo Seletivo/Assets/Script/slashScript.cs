using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashScript : MonoBehaviour
{
    public int dano_slash;
    public float destroyTime = 0.1f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inimigo otherInimigo = collision.GetComponent<Inimigo>();
        if (otherInimigo != null)
        {
            otherInimigo.LevouDano(dano_slash);
        }
        if (collision.CompareTag("Inimigo"))
        {
            Destroy(gameObject);
        }
    }
}

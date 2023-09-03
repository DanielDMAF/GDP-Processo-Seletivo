using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float vel_bullet;
    public int dano_bullet = 1;
    private float destroyTime = 2f;


    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * vel_bullet * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inimigo otherInimigo = collision.GetComponent<Inimigo>();
        if (otherInimigo != null)
        {
            otherInimigo.LevouDano(dano_bullet);
        }
        Destroy(gameObject);
    }
}

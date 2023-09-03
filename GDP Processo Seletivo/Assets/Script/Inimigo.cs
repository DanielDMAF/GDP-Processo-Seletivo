using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    //Necessario do inimigo
    public int vida;
    public float vel_opp;
    public float attackDistance;
    protected Animator anim;
    protected bool facingRight = true;
    protected Rigidbody2D rb;
    
    //Alvo
    protected Transform alvo;
    protected float alvoDistance;

    //Pos morte
    public GameObject ammo;
    //public GameObject morteAnimation;
    protected SpriteRenderer sprite;


    void Awake()
    {
        anim = GetComponent<Animator>(); //Self
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        alvo = FindObjectOfType<Player>().transform; //Alvo
    }

    
    protected virtual void Update()
    {
        alvoDistance = transform.position.x - alvo.position.x;

      

    }
    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void LevouDano(int dano)
    {
        vida -= dano;
        if (vida < 0)
        {
            Instantiate(ammo, transform.position,transform.rotation);
            //Instantiate(morteAnimation, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(LevouDanoRoutine());
        }
    }

    IEnumerator LevouDanoRoutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;

    }
}

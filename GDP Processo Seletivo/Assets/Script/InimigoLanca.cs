using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoLanca : Inimigo
{
    

    public float walkDistance; //Distacia ate o inimigo 
    private bool walk;

    private float attackTime; //estado de attack
    private bool attack;

    void Start()
    {
        attackTime = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        anim.SetBool("Walk", walk);
        attackTime -= Time.deltaTime;
        if (attackTime < 0) 
        {
            attack = true;
        }

        if (Mathf.Abs(alvoDistance) < walkDistance)
        {
            walk = true;
        }
        if ((Mathf.Abs(alvoDistance) < attackDistance) && attack)
        {
            anim.SetTrigger("Ataque");
            walk = false;
            attackTime = 1.3f;
            attack = false;
        }
    }
    private void FixedUpdate()
    {
        if (walk && attack)
        {
            if (alvoDistance < 0)
            {
                rb.velocity = new Vector2(vel_opp, rb.velocity.y);
                if (!facingRight)
                {
                    Flip();
                }
            }
            else
            {
                rb.velocity = new Vector2(-vel_opp, rb.velocity.y);
                if (facingRight)
                {
                    Flip();
                }
            }
        }
        
    }

}

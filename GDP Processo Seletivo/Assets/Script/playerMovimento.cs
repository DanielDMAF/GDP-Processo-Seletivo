using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovimento : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask chaoLayer;
    [SerializeField] Transform chaoCheck;

    private Animator anim;

    private bool facingRight = true;
    public float vel_Mov;
    private float mov_hor = 0;

    public float forca_pulo; //pulo
    public bool Jumping;
    private float coyoteDelay = 0.2f; //Coyote time pulo
    private float coyoteTimer;

    private float mov_vert = 0;
    private bool crouch;
    private bool lookUp;

    private bool reload;

    



    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
        //Parte Pulo:
        if (NoChao())
        {
            coyoteTimer = coyoteDelay;
            anim.SetBool("Jump", false);
            Jumping = false;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        if ((Input.GetButtonDown("Jump") && coyoteTimer > 0) && (!lookUp && !crouch && !reload)) //pula com a adiçao dp coyote timer
        {
            rb.velocity = new Vector2(rb.velocity.x, forca_pulo);
            Jumping = true;
        }
        if (Input.GetButtonUp("Jump") && (rb.velocity.y > 0)) //faz o pulo ser menor caso solte o botao
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimer = 0f;
        }

        //Tiro parte:
        if (Input.GetKeyDown(KeyCode.Z)) 
        {

            anim.SetTrigger("Shoot"); //Animation
        }

        //Reload//Slash parte
        if (Input.GetKeyDown(KeyCode.X))
        {
            Slash();
        }

        //Input Horizontais:
        lookUp = Input.GetKey(KeyCode.UpArrow);
        crouch = Input.GetKey(KeyCode.DownArrow);

        //Parte pegar input dos movimentos horizontais
        if (!lookUp && !crouch && !reload)
        {
            mov_hor = Input.GetAxis("Horizontal");
        }
        else if (NoChao())
        {
            mov_hor = 0;
        }
        //Animations parte:
        anim.SetFloat("Speed", Mathf.Abs(mov_hor));
        if (Jumping)
        {
            anim.SetBool("Jump", true);
        }
        anim.SetBool("LookUP", lookUp);
        anim.SetBool("Crouch", crouch);
        anim.SetBool("LookDown", crouch);
    }

    private void FixedUpdate()
    {
        //Parte movimentar player horizontalmente
        rb.velocity = new Vector2(vel_Mov * mov_hor, rb.velocity.y);

        //Parte flip sprite:
        if ((facingRight && mov_hor < 0) || (!facingRight && mov_hor > 0))
        {
            Flip();
        }
    }


    private bool NoChao()
    {
        return Physics2D.OverlapCircle(chaoCheck.position, 0.2f, chaoLayer);
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void Slash()
    {
        //anim.SetBool("Reload", true);
    }
}

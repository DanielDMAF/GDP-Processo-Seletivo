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
    [SerializeField] SpriteRenderer sprite;
    private Animator anim;

    //mov horiizontal
    private bool facingRight = true;
    public float vel_Mov;
    private float mov_hor = 0;

    public float forca_pulo; //pulo
    public bool Jumping;
    private float coyoteDelay = 0.2f; //Coyote time pulo
    private float coyoteTimer;

    //private float mov_vert = 0;
    private bool crouch;
    private bool lookUp;

    //Reload/slash:
    private bool slash;
    public GameObject slashPrefab;
    public Transform slashSpawn;
    private float slashCooldown = 0.3f;
    private float nextSlash;

    //Fire:
    public static int ammoMax;
    public static int ammo;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform bulletSpawnDown;
    private float fireRate = 0.5f;
    private float nextFire;

    //Vida:
    public static int vida;
    public static int vidaMax;



    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        nextFire = 0;
        nextSlash = 0;
        ammo = 5;
        ammoMax = 5;
        vidaMax = 10;
        vida = 10;
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
            coyoteTimer -= Time.deltaTime; //Utiliza coyote time
        }
        if ((Input.GetButtonDown("Jump") && coyoteTimer > 0) && (!lookUp && !crouch && !slash)) //pula com a adiçao dp coyote timer
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
        if (Input.GetKeyDown(KeyCode.Z) && (Time.time > nextFire) && ammo>0)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }

        //Reload//Slash parte
        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextSlash)
        {
            nextSlash = Time.time + slashCooldown;
            Slash();
        }

        //Input Verticais:
        lookUp = Input.GetKey(KeyCode.UpArrow);
        crouch = Input.GetKey(KeyCode.DownArrow);

        //Parte pegar input dos movimentos horizontais
        if (!lookUp && !crouch && !slash)
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

    //Colisao controle:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            StartCoroutine(LevouDano());
        }
        if (collision.gameObject.CompareTag("Vida"))
        {
            vida = vidaMax;
        }


    }

    IEnumerator LevouDano()
    {
        vida -= 1;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
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
        anim.SetTrigger("Reload"); //Animation
        GameObject tempSlash = Instantiate(slashPrefab, slashSpawn.position, slashSpawn.rotation);
        if (!facingRight)
        {
            tempSlash.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Fire()
    {
        anim.SetTrigger("Shoot"); //Animation
        ammo -= 1;
        if (crouch && !NoChao())
        {
            GameObject tempBullet = Instantiate(bulletPrefab, bulletSpawnDown.position, bulletSpawnDown.rotation);
            tempBullet.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            GameObject tempBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            if (!facingRight && lookUp && NoChao())
            {
                tempBullet.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (!facingRight)
            {
                tempBullet.transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
    }
}

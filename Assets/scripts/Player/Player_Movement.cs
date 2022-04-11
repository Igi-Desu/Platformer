using System.Collections;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    //important variables that affect how the player moves and with which object will interact
    [SerializeField] float speed = 5;
    [SerializeField] float basespeed = 5;
    [SerializeField] float jumppower = 12;
    [SerializeField] Transform groundcheck;
    [SerializeField] HUD hud;
    [SerializeField] GameObject DeathAnim;
    [SerializeField] Transform wallcheck;
    [SerializeField] Transform ceilingcheck;
    [SerializeField] LayerMask whatisground;
    [SerializeField] float wallslidingspeed;
    [SerializeField] Gamemanager gamemanager;
    [SerializeField] ParticleSystem walkParticles;
    [SerializeField] ParticleSystem landparticles;
    [SerializeField] ParticleSystem bloodparticles;
    ParticleSystem.EmissionModule particleemission;
    ParticleSystem.EmissionModule particleemissiononland;
 //   ParticleSystem.EmissionModule particleemissiononblood;
    [SerializeField] Flashscreen flashyscreen;
    //helpful variables to determinite status but changing them won't affect much gameplay
    const float groundcheckradius = 0.15f;
    const float iframebase = 2f;
    float iframe = 0;
    float coyotejumpbase = 0.2f;
    float coyotejump;
    bool facingright = true;
    bool grounded = true;
    bool shouldtakedmg = false;
    bool walled = false;
    bool jump = false;
    bool walljump = false;
    bool crouch;
    float walljumpcd;
    int hp = 3;
    [SerializeField] float walljumpcdbase = 1f;
    float xmov;
    int stamina = 100;
    //components
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriterenderer;
    BoxCollider2D topcollider;
    private void Start()
    {
        particleemission = walkParticles.emission;
        particleemissiononland = landparticles.emission;
      //  particleemissiononblood = bloodparticles.emission;
        coyotejump = coyotejumpbase;
        crouch = false;
        topcollider = GetComponent<BoxCollider2D>();
        walljumpcd = walljumpcdbase;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
         //guard clause
        if (Gamemanager.cutscene == true) return;
        walljumpcd -= Time.deltaTime;
        iframe -= Time.deltaTime;
        if (shouldtakedmg && iframe < 0)
        {
            take_dmg();
        }
        xmov = 0;
        if (!grounded)
        {
            //if we are not on the ground the coyote jump timer will begin
            //coyote jump is a mechanic that makes jumping better since we can jump even short moment after
            //leaving ground
            coyotejump -= Time.deltaTime;
        }
        //moving left and right
        if (Input.GetKey(KeyCode.A))
        {
            if (facingright) flip();
            xmov = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!facingright) flip();
            xmov = 1;
        }
        //if we want to jump
        if (Input.GetKeyDown(KeyCode.Space) && !crouch)
        {
            if (coyotejump > 0)
            {
                animator.SetBool("Jumping", true);
               jump = true;
            }
            else if (walljump && walljumpcd <= 0)
            {
                walljumpcd = walljumpcdbase;
                animator.SetBool("Jumping", true);
                jump = true;
            }
        }
        else if (Input.GetKey(KeyCode.S) && grounded)
        {
            topcollider.enabled = false;
            crouch = true;
        }
        else
        {
            if (crouch)
            {
                if (!Physics2D.OverlapCircle(ceilingcheck.position, groundcheckradius, whatisground))
                {
                    topcollider.enabled = true;
                    crouch = false;
                }
            }
        }
        //if we release jump during upward movement slow down jump
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }
    private void FixedUpdate()
    {
        if (Gamemanager.cutscene == true) return;
        //change animation
        animator.SetFloat("Speed", Mathf.Abs(xmov));
        animator.SetBool("Crouch", crouch);
        speed = crouch ? basespeed * 0.5f : basespeed;
        rb.velocity = new Vector2(xmov * speed, rb.velocity.y);

        bool wasgrounded = grounded;
        grounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, whatisground);
        walled = Physics2D.OverlapCircle(wallcheck.position, groundcheckradius, whatisground);
        if (grounded)
        {
            if (xmov != 0&&!crouch)
            {
                particleemission.rateOverTime = 20;
            }
            //if the player just landed
            if (!wasgrounded)
            {
                coyotejump = coyotejumpbase;
                particleemissiononland.rateOverTime = 70;
                StartCoroutine(disableafterx(particleemissiononland, 0.1f));
                animator.SetBool("Jumping", false);
            }
        }
        if(!grounded||xmov==0||crouch)
        {
            particleemission.rateOverTime = 0;
        }
        if (walled && xmov != 0 && stamina > 0)
        {
            stamina = Mathf.Clamp(stamina - 1, 0, 100);
            if (stamina < 30)
            {
                Color c = Color.red;
                c.a = spriterenderer.color.a;
                spriterenderer.color = c;
            }
            walljump = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallslidingspeed, float.MaxValue));
        }
        else if (grounded)
        {
            Color c = Color.white;
            c.a = spriterenderer.color.a;
            spriterenderer.color = c;
            stamina = 100;
        }
        else if (!walled || stamina == 0)
        {
            walljump = false;
        }
        if (jump)
        {
            FindObjectOfType<audiomanager>().Play("Jump");
            rb.velocity = Vector2.up * jumppower;
            coyotejump = -1;
            jump = false;
        }

    }
    IEnumerator disableafterx(ParticleSystem.EmissionModule e, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        e.rateOverTime = 0;
    }

    public void ResetVariables()
    {
        rb.velocity = Vector2.zero;
        xmov = 0;
        jump = false;
        walljump = false;
        animator.SetFloat("Speed", 0);
        animator.SetBool("Jumping", false);
        particleemission.rateOverTime = 0;
    }
    void flip()
    {
        facingright = !facingright;
        if (facingright)
        {
            //    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //StartCoroutine(rotatesmooth(Vector3.zero));
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            //   transform.rotation = Quaternion.Euler(Vector3.zero);
            //  StartCoroutine(rotatesmooth(new Vector3(0,180,0)));
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator rotatesmooth(Vector3 dest)
    {
        yield return new WaitForEndOfFrame();
        Vector3 cur = transform.rotation.eulerAngles;
        cur = Vector3.MoveTowards(cur, dest, Time.deltaTime * 1500);
        transform.rotation = Quaternion.Euler(cur);
        if (cur != dest)
        {
            StartCoroutine(rotatesmooth(dest));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy"|| collision.transform.tag == "Hazard")
        {
            shouldtakedmg = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.transform.tag == "Enemy")
        {
            //check if enemy was hit from above
            RaycastHit2D[] hit =Physics2D.RaycastAll(transform.position, Vector2.down, 2f);

            foreach (var rayhit in hit)
            {
                if (rayhit.transform.CompareTag("Enemy"))
                {
                    //bounce
                    rb.velocity = new Vector2(rb.velocity.x, 12.5f);
                    //this one is also OK but the faster player is falling the lower they will bounce
                   // rb.AddForce(new Vector2(0, 500));
                    Destroy(rayhit.transform.gameObject);
                    return;
                }
            }
            if (iframe < 0)
            {
                shouldtakedmg = true;
                return;
            }
        }
        if (collision.transform.tag == "Hazard")
        {
                shouldtakedmg = true;
            //or either just kill player
            //bloodparticles.Play();
            //flashyscreen.Flash(0.2f, 0.9f, new Color(155, 0, 0));
            //  hud.deletealllifes();
            // respawn();
            return;
        }
        if (collision.transform.tag == "Death")
        {
            FindObjectOfType<audiomanager>().Play("PlayerHurt");
            bloodparticles.Play();
            flashyscreen.Flash(0.2f, 0.9f, new Color(155, 0, 0));
            hud.deletealllifes();
            respawn();
            return;
        }
        if (collision.transform.tag == "Levelend")
        {
            gamemanager.levelend();
        }
    }
    void take_dmg()
    {
        //add knockback
        FindObjectOfType<audiomanager>().Play("PlayerHurt");
        iframe = iframebase;
        StartCoroutine(Iframes());
        animator.Play("Player_Hurt");
        bloodparticles.Play();
        flashyscreen.Flash(0.2f, 0.7f, new Color(155, 0, 0));
        hud.removelife(hp);
        hp--;
        if (hp == 0)
        {
            respawn();
        }
    }
    public void increaselife()
    {
        if (hp == 3) return;
        hud.addlife(hp);
        hp++;
    }
    void respawn()
    {
        Destroy(gameObject);
        
    }
    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            Instantiate(DeathAnim, transform.position, DeathAnim.transform.rotation);
        }
        gamemanager.Respawn();
    }
    IEnumerator Iframes()
    {
        Color c = spriterenderer.color;
        while (iframe > 0)
        {
            c.a = (c.a == 1) ? 0.25f : 1;
          //  Debug.Log("Alpha " + c.a);
            spriterenderer.color = c;
            yield return new WaitForSeconds(0.0625f);
        }
        c.a = 1;
        spriterenderer.color = c;
    }
}

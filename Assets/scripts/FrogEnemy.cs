using UnityEngine;

public class FrogEnemy : Base_Enemy
{
    
    float jumptimer;
    const float jumptimerbase = 3;
    Rigidbody2D rb;
    float jumpforce = 250;
    Animator anim;
    bool facingright = false;
 
    [SerializeField]GameObject player;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumptimer = jumptimerbase;
    }

    void Update()
    {
        if (player == null) return;
        jumptimer -= Time.deltaTime;
        //simple jump towards player
        if (jumptimer < 0)
        {
            jumptimer = jumptimerbase;
            anim.Play("Frog_Jump");
            if (player.transform.position.x>transform.position.x)
            {
                jump(true);
                if (!facingright) { flip(); }
            }
            else
            {
                jump(false);
                if (facingright) { flip(); }
            }
        }
    }

    void jump(bool jumpingright)
    {
        float xmov = jumpingright ? 150 : -150;
        rb.AddForce(new Vector2(xmov, jumpforce));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.Play("Frog_Idle");
    }
    void flip()
    {
        facingright = !facingright;
        if (facingright)
        {
            //    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //StartCoroutine(rotatesmooth(Vector3.zero));
            transform.rotation = Quaternion.Euler(0, 180, 0);
           
        }
        else
        {
            //   transform.rotation = Quaternion.Euler(Vector3.zero);
            //  StartCoroutine(rotatesmooth(new Vector3(0,180,0)));
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
   
}

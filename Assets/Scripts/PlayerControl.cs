using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D rb2D;

    public float health;
    public float previousHealth;
    public float maxHealth;

    public Image filler;

    [SerializeField]
    float counter;
    [SerializeField]
    float maxCounter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //Debug.Log(Input.GetAxisRaw("Horizontal"));
            transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb2D.linearVelocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");

        }

        if (counter > maxCounter)
        {
            previousHealth = health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(previousHealth/maxHealth, health/maxHealth, counter/maxCounter);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            TakeDamage(20);

        }
    }

    void TakeDamage(float dmg)
    {
        previousHealth = filler.fillAmount * maxHealth;
        counter = 0;
        health -=dmg;
    }
}


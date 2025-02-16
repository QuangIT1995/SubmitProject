using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float speed = 5f;
    public float jumpForce = 8f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.5f;
    private float defaultSpeed;
    private bool isDashing = false;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        Instance = this;
        defaultSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float move = Input.GetAxis("Horizontal"); // A/D hoặc Phím Trái/Phải
        anim.SetFloat("speed", Mathf.Abs(move));
        transform.Translate(Vector3.right * move * speed * Time.deltaTime);

        if(move > 0) spriteRenderer.flipX = true;
        if(move < 0) spriteRenderer.flipX = false;

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Player is jumping");
            Jump();
            AudioManager.Instance.PlayJump();
            //Add method Jump here
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            Debug.Log("Player is dashing");
            AudioManager.Instance.PlayDash();
            StartCoroutine(Dash());
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    private IEnumerator Dash(){
        isDashing = true;
        speed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        speed = defaultSpeed;
        isDashing = false;
    }

    public void BoostSpeed()
    {
        speed = 8f;
        Invoke("ResetSpeed",5f);
    }

    private void ResetSpeed()
    {
        speed = defaultSpeed;
    }

    public void ApplyDebuff(string debuffType)
    {
        switch(debuffType)
        {
            case "Slowdown":
                speed = 2f;
                Invoke("ResetSpeed",1f);
                break;
            case "ReservesControl":
                StartCoroutine(ReservesControl(5f));
                break;
            case "WeakJump":
                jumpForce = 3f;
                Invoke("ResetJumpForce",5f);
                break;
        }
    }

    private IEnumerator ReservesControl(float duration)
    {
        float originalSpeed = speed;
        speed = -speed;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }

    private void ResetJumpForce()
    {
        jumpForce = 8f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mushroom"))
        {
            AudioManager.Instance.PlayCollect();
            if(GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(10);
            }
            other.gameObject.SetActive(false); // Ẩn nấm thay vì destroy (dùng Object Pooling)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

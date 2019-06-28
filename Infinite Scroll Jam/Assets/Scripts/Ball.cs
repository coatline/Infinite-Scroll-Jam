using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float bounceForce;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject deathMenu;
    Rigidbody2D rb;
    Animator a;

    void Start()
    {
        a = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    int score;

    void Update()
    {
        var inputs = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(inputs * speed, rb.velocity.y);

        if (transform.position.x > 8.75f)
        {
            transform.position = new Vector3(-8.75f, transform.position.y);
        }
        else if (transform.position.x < -8.75f)
        {
            transform.position = new Vector3(8.75f, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {

            if (rb.velocity.y <= 0)
            {
                if (!collision.gameObject.GetComponent<Platform>().bouncedOn)
                {
                    collision.gameObject.GetComponent<Platform>().bouncedOn = true;

                    score++;
                    scoreText.text = $"{score}";
                    var stA = scoreText.GetComponent<Animator>();
                    stA.ResetTrigger("ScorePoint");
                    stA.SetTrigger("ScorePoint");
                }

                rb.AddForce(new Vector2(0, bounceForce));
                a.ResetTrigger("Bounce");
                a.SetTrigger("Bounce");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        deathMenu.gameObject.SetActive(true);
        deathMenu.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"You Scored: {score}";
        Destroy(gameObject);
    }
}

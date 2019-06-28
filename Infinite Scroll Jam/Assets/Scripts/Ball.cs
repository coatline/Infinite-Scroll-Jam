using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject[] platforms;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float bounceForce;
    [SerializeField] float speed;
    public GameObject deathMenu;
    public int score;
    Rigidbody2D rb;
    Animator a;

    void Start()
    {
        a = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;


    }


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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
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
        else if (collision.gameObject.CompareTag("Spikes"))
        {
            deathMenu.gameObject.SetActive(true);
            deathMenu.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"You Scored: {score}";
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spikesPrefab;
    [SerializeField] float maxDifficulty = 5;
    [SerializeField] float difficulty = 1;
    [SerializeField] Camera cam;
    [SerializeField] Ball ball;

    void Update()
    {
        transform.position = cam.transform.position - new Vector3(0, 0, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            var amount = 16;
            var script = collision.gameObject.GetComponent<Platform>();
            if (script.keepX)
            {
                amount += 2;
            }
            collision.gameObject.transform.position += new Vector3(0, amount);
            collision.gameObject.transform.position = new Vector3(Random.Range(-8f, 8f), collision.gameObject.transform.position.y);
            script.keepX = false;
            script.bouncedOn = false;

            if (Random.Range(0, maxDifficulty * 2) <= difficulty && collision.gameObject.transform.childCount > 0)
            {
                Instantiate(spikesPrefab, collision.gameObject.transform.GetChild(0).transform.position, Quaternion.identity);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            var ballScript = collision.gameObject.GetComponent<Ball>();
            ballScript.deathMenu.gameObject.SetActive(true);
            ballScript.deathMenu.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"You Scored: {ballScript.score}";
            ball.gameObject.SetActive(false);
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

}

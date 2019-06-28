using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
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
            collision.gameObject.transform.position += new Vector3(0, 16);
            collision.gameObject.transform.position = new Vector3(Random.Range(-8f, 8f), collision.gameObject.transform.position.y);
            collision.gameObject.GetComponent<Platform>().bouncedOn = false;
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

}

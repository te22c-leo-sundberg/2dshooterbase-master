using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoltController : MonoBehaviour
{
    [SerializeField]
    float speed = 25f;
    public bool isExploding = false;

    [SerializeField]
    GameObject boltExplosion;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.up;

        transform.Translate(movement * speed * Time.deltaTime);

        if (transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            if (isExploding)
            {
                Instantiate(boltExplosion, transform.position, Quaternion.identity);
            }
        }
    }
}

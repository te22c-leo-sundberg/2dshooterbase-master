using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speedY;

    [SerializeField]
    float speedX;
    [SerializeField]
    float dropRate;

    [SerializeField]
    float timeBetweenShot;

    [SerializeField]
    GameObject explosionPrefab;

    [SerializeField]
    GameObject explosionBoosterPrefab;

    [SerializeField]
    GameObject shieldBoosterPrefab;
    
    [SerializeField]
    GameObject shootingBoosterPrefab;



    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = new();
        position.y = Camera.main.orthographicSize + 1;
        position.x = Random.Range(-7f, 7f);

        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.down;

        // movement.x = Mathf.Sin(transform.position.y * speedX);

        transform.Translate(movement * speedY * Time.deltaTime);

        if (transform.position.y < -Camera.main.orthographicSize - 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ship")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            GameObject player = GameObject.FindGameObjectWithTag("Ship");

            ShipController controller = player.GetComponent<ShipController>();

            controller.AddPoints(-20);
        }
        else if (other.gameObject.tag == "Bolt" || other.gameObject.tag == "BoltExplosion")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            GameObject player = GameObject.FindGameObjectWithTag("Ship");

            ShipController controller = player.GetComponent<ShipController>();

            controller.AddPoints(10);

            if (Random.Range(1,11) * dropRate >= 10)
            {
                int powerUpChance = Random.Range(1,4);
                if (powerUpChance == 1)
                {
                    Instantiate(explosionBoosterPrefab, transform.position, Quaternion.identity);
                }
                else if (powerUpChance == 2)
                {
                    Instantiate(shieldBoosterPrefab, transform.position, Quaternion.identity);
                }
                else if (powerUpChance == 3)
                {
                    Instantiate(shootingBoosterPrefab, transform.position, Quaternion.identity);
                }
            }

        }
    }
}

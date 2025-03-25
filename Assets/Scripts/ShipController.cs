using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    GameObject boltPrefab;

    [SerializeField]
    Transform gunPosition;

    [SerializeField]
    GameObject shieldSprite;

    [SerializeField]
    float timeBetweenShots = 0.5f;
    float timeSinceLastShot = 0;

    [SerializeField]
    int maxHealth = 5;
    int currentHealth;
    int shield;

    [SerializeField]
    Slider hpBar;

    [SerializeField]
    TMP_Text pointsText;
    public static int points = 0;

    bool explodingShots = false;
    float explosionPowerUpTimer = 0;
    bool doubleShots = false;
    float shootingPowerUpTimer = 0;


    void Start()
    {
        currentHealth = maxHealth;
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;

        AddPoints(0);
    }

    void Update()
    {

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(xInput, yInput).normalized * speed * Time.deltaTime;

        transform.Translate(movement);

        timeSinceLastShot += Time.deltaTime;

        if (Input.GetAxisRaw("Fire1") > 0 && timeSinceLastShot > timeBetweenShots)
        {
            timeSinceLastShot = 0;
            Instantiate(boltPrefab, gunPosition.position, Quaternion.identity);
        }

        if (explosionPowerUpTimer > 0)
        {
            explodingShots = true;
        }
        else
        {
            explodingShots = false;
        }

        if (shootingPowerUpTimer > 0)
        {
            doubleShots = true;
        }
        else
        {
            doubleShots = false;
        }

        if (shield > 0)
        {
            shieldSprite.SetActive(true);
        }
        else
        {
            shieldSprite.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            print(currentHealth);
            currentHealth--;
            hpBar.value = currentHealth;
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    // public void UpdatePoints(int points)
    // {
    //     points ++;
    // }
    public void AddPoints(int amount)
    {
        points += amount;
        pointsText.text = points.ToString();
    }
    public void PowerUp(string boostType)
    {
        if (boostType == "Explosion")
        {
            explosionPowerUpTimer += 30;
        }
        else if (boostType == "Shield")
        {
            shield ++;
        }
        else if (boostType == "Shooting")
        {
            shootingPowerUpTimer += 30;
        }
    }
}
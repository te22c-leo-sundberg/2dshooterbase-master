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
    float shootingPowerUpTimer = 0;

    [SerializeField]
    GameObject explosionBuff;
    [SerializeField]
    GameObject shieldBuff;
    [SerializeField]
    GameObject shootingBuff;


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
            var bolt = Instantiate(boltPrefab, gunPosition.position, Quaternion.identity);
            boltPrefab.GetComponent<BoltController>().isExploding = explodingShots;
        }


        if (explosionPowerUpTimer > 0)
        {
            explosionBuff.SetActive(true);
            explodingShots = true;
            explosionPowerUpTimer -= 1 * Time.deltaTime;
        }
        else if (explosionPowerUpTimer <= 0)
        {
            explodingShots = false;
            explosionBuff.SetActive(false);
        }

        if (shootingPowerUpTimer > 0)
        {
            shootingBuff.SetActive(true);
            timeBetweenShots = 0.25f;
            shootingPowerUpTimer -= 1 * Time.deltaTime;
        }
        else if (shootingPowerUpTimer <= 0)
        {
            shootingBuff.SetActive(false);
            timeBetweenShots = 0.5f;
        }

        if (shield > 0)
        {
            shieldBuff.SetActive(true);
            shieldSprite.SetActive(true);
        }
        else
        {
            shieldBuff.SetActive(false);
            shieldSprite.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (shield <= 0)
            {
                currentHealth--;
                hpBar.value = currentHealth;
                if (currentHealth <= 0)
                {
                    SceneManager.LoadScene(1);
                }
            }
            else
            {
                shield--;
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
            shield++;
        }
        else if (boostType == "Shooting")
        {
            shootingPowerUpTimer += 30;
        }
    }
}
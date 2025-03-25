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
    float timeBetweenShots = 0.5f;
    float timeSinceLastShot = 0;

    [SerializeField]
    int maxHealth = 5;
    int currentHealth;

    [SerializeField]
    Slider hpBar;

    [SerializeField]
    TMP_Text pointsText;
    public static int points = 0;

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
}
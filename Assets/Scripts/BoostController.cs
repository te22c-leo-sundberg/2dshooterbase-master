using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    string boostType;

    void Update()
    {
        Vector2 movement = -1 * Vector2.up;

        transform.Translate(movement * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ship")
        {
            Destroy(this.gameObject);

            GameObject player = GameObject.FindGameObjectWithTag("Ship");
            ShipController controller = player.GetComponent<ShipController>();

            controller.PowerUp(boostType);
        }
    }
}

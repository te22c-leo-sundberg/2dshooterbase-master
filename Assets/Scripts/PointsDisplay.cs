using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text pointsText;
    void Start()
    {
        pointsText.text = ShipController.points.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image healthImage;
    private float maxHealth = 0.0f;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = gameObject.GetComponent<UnitHealth>().health;
        rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.transform.parent.transform.rotation = rotation;
        healthImage.fillAmount = gameObject.GetComponent<UnitHealth>().health / maxHealth;
    }
}

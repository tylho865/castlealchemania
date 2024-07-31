using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwPotion : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject spell;
    public float launchVelocity = 30.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var _spell = Instantiate(spell, launchPoint.position, launchPoint.rotation);
            Rigidbody2D rb = _spell.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = launchPoint.up * launchVelocity; 
            }
        }
    }
}

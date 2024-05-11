using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpickyAction : BaseHazzards
{
    [SerializeField] private GameObject objectToSwitch;
    [SerializeField] private GameObject spickObject;

    [SerializeField] private int time;
    [SerializeField] private float currentTime;
    [SerializeField] private bool switchFace = false;

    private void Start()
    {
        currentTime = time;
    }

    public override void ABMovements()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 && switchFace == false)
        {
            objectToSwitch.SetActive(false);
            spickObject.SetActive(true);
            switchFace = true;
            currentTime = time;
        }
        else if (currentTime <= 0 && switchFace == true)
        {
            objectToSwitch.SetActive(true);
            spickObject.SetActive(false);
            switchFace = false;
            currentTime = time;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealthComponent))
        {
            if (switchFace == true)
            {
                playerHealthComponent.takeDamage(damage);
            }
        }
    }
}

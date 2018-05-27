using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour {

    [SerializeField]
    private int currentHealth = 3;
    public float colourChangeDelay = 0.5f;
    float currentDelay = 0f;
    private bool colourChangeCollision = false;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Contact was made!");
        colourChangeCollision = true;
        currentDelay = Time.time + colourChangeDelay;
    }
    void checkColourChange()
    {
        if (colourChangeCollision)
        {
            transform.GetComponent<Renderer>().material.color = Color.yellow;
            if (Time.time > currentDelay)
            {
                transform.GetComponent<Renderer>().material.color = Color.white;
                colourChangeCollision = false;
            }
        }
    }

    void Update()
    {
        checkColourChange();
    }


    public void Damage(int _damage)
    {
        currentHealth -= _damage;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

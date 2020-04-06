using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    public float moveSpeed = 5f;

    private GameObject enemy;
    private Enemy enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        playerCollider.height = 1f;
        playerCollider.center = new Vector3(0f, 0.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var moveVector = new Vector3(moveHorizontal, 0f, moveVertical);

        transform.Translate(moveVector * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision collisionWith)
    {
        if (collisionWith.gameObject.tag == "Enemy")
        {
            collisionWith.gameObject.GetComponent<Enemy>().enemyHealth--;
        }
        
    }
}

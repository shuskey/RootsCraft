using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool openable = false;
    private Animator anim;
    private bool chestOpenState = false;

    public Rigidbody coinPrefab;
    public Transform spawner;


    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openable && Input.GetKeyDown(KeyCode.Space))
        {
            chestOpenState = !chestOpenState;
            anim.SetBool("openChest", chestOpenState);
            if (chestOpenState)
            {
                Rigidbody coinInstance;
                coinInstance = Instantiate(coinPrefab, spawner.position, spawner.rotation) as Rigidbody;
                coinInstance.AddForce(spawner.up * 100);
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            openable = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            openable = false;
        }

    }
}

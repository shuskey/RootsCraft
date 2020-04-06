using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetActivity : MonoBehaviour
{
    public float gravity = -10.0f;
    public float cloudSpeed = 0.03f;
    public float currentYear = 0;
    public Vector3 oneYearScale = new Vector3(0.2f, 0.2f, 0.2f);

    public void Awake()
    {
        currentYear = Mathf.Round(transform.localScale.y / 2 * 10);
    }

    public void AttractMe(GameObject gameObject)
    {
        float targetDistance = (gameObject.transform.position - transform.position).magnitude;
        Vector3 planetDirection = (gameObject.transform.position - transform.position).normalized;        

        gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, planetDirection) * gameObject.transform.rotation;        
        gameObject.GetComponent<Rigidbody>().AddForce(planetDirection * gravity);
    }
    public void FloatMe(GameObject gameObject)
    {
        Vector3 planetDirection = (gameObject.transform.position - transform.position).normalized;
        
        gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, planetDirection) * gameObject.transform.rotation;
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * cloudSpeed);
        gameObject.GetComponent<Rigidbody>().AddForce(planetDirection * cloudSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            transform.localScale -= oneYearScale;
            currentYear = Mathf.Round(transform.localScale.y / 2 * 10);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        { 
            transform.localScale += oneYearScale;
            currentYear = Mathf.Round(transform.localScale.y / 2 * 10);
        }
    }
}

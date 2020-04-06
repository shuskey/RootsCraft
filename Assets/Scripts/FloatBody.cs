using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatBody : MonoBehaviour
{
    PlanetActivity planet;
    void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetActivity>();
        var rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
    private void FixedUpdate()
    {
        planet.FloatMe(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


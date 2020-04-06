using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivitX = 250f;
    public float mouseSensitivitY = 250f;
    public float walkSpeed = 10f;
    public float jumpForce = 220f;
    public LayerMask groundedMask;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    Transform cameraT;
    float verticalLookRotation;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        cameraT = Camera.main.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("RightStickHorizontal") * Time.deltaTime * mouseSensitivitX);
        verticalLookRotation += Input.GetAxis("RightStickVertical") * Time.deltaTime * mouseSensitivitY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60f, 60f);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        //Vector3 moveDir = new Vector3(Input.GetAxisRaw("RightStickHorizontal"), 0, Input.GetAxisRaw("RightStickVertical")).normalized;
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f);

        if (Input.GetButtonDown("Jump")) // && grounded)
        {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.up * jumpForce);
        }
        
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        grounded = (Physics.Raycast(ray, out hit, 1 + 0.1f, groundedMask));
        
    }

    private void FixedUpdate()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);        
    }
}

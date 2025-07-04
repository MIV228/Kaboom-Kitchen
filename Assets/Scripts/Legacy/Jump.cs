using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    public InputActionProperty restartAction;
    public Vector3 lastCheckpoint;
    public GameObject xrOrigin;

    public InputActionProperty jumpAnimationAction;
    public Rigidbody rb;
    public float jumpForce;

    public float groundDrag;

    public bool jump;

    public bool isGrounded;
    public Transform feetPoint;

    public bool canJump;
    public float maxCD;
    public float CD;
    
    void Update()
    {
        isGrounded = Physics.Raycast(feetPoint.position, Vector3.down, 0.15f);
        if (CD > 0)
        {
            CD -= Time.deltaTime;
            canJump = false;
        }
        else
        {
            canJump = true;
        }

        if (isGrounded)
        {
            rb.drag += Time.deltaTime * 5;
            if (rb.drag > groundDrag) rb.drag = groundDrag;
        }
        else rb.drag = 0;

        float triggerValue = jumpAnimationAction.action.ReadValue<float>();
        if (triggerValue > 0 && isGrounded && canJump)
        {
            jump = true;
        }
        
        if (jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            CD = maxCD;
            jump = false;
        }

        float triggerValueR = restartAction.action.ReadValue<float>();
        if (triggerValueR > 0)
        {
            if (lastCheckpoint != null)
            {
                transform.position = lastCheckpoint;
                rb.velocity = Vector3.zero;
            }
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

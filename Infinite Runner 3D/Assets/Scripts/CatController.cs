using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] BoxCollider normalCollider;
    [SerializeField] BoxCollider rollCollider;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] List<float> runningLines = new List<float>();
    [SerializeField] int startingLine;
    [SerializeField] float movespeed;
    [SerializeField] float transitionSpeed;
    [SerializeField] float jumpStrength;
    private int currentLine;
    private bool inTransition;


    private void Start()
    {
        SwitchLine(startingLine);
    }

    private void Update()
    {
       
        if (Input.GetButtonDown("Horizontal"))
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal > 0)
            {
                SwitchLine(currentLine + 1);
            }
            else
            {
                SwitchLine(currentLine - 1);
            }
        }

        if (Input.GetButtonDown("Vertical") && !inTransition)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            if (vertical > 0)
            {
                Jump();
            }
            else
            {
                Roll();
            }
        }

        Vector3 targetPosition = transform.position;
        targetPosition.x = runningLines[currentLine];
        targetPosition.z += movespeed;
        if (Vector2.Distance(transform.position, targetPosition) < 0.3)
        {
            inTransition = false;
        }


        rb.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
    }

    public void SwitchLine(int number)
    {
        if (number >= runningLines.Count || number < 0)
        {
            Debug.LogError("Given number is out of index");
            return;
        }
        currentLine = number;
        inTransition = true;
    }

    public void Roll()
    {
        anim.Play("Roll");
        rollCollider.enabled = true;
        normalCollider.enabled = false;
    }

    // se povikuva preku AnimationEvent
    public void FinishRoll()
    {
        normalCollider.enabled = true;
        rollCollider.enabled = false;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        anim.Play("Jump");
    }
}

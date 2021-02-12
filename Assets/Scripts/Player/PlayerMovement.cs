using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public PlayerMovementInput movementInput;

    private NavMeshAgent navMeshAgent;
    public LayerMask whatCanBeClickedOn;


    public float movementSpeed = 1f;

    Rigidbody rbody;

    Vector2 movementVector;
    bool lastActionWasClick = false;
    public bool holdingMouseButton = false;

    private Vector3 destination;
    // public GridLayout grid;
    Camera mainCamera;



    void Awake()
    {
        movementInput = new PlayerMovementInput();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;

        rbody = GetComponent<Rigidbody>();

        mainCamera = Camera.main;

    }

    void OnEnable()
    {
        movementInput.Enable();
    }

    void OnDisable()
    {
        movementInput.Disable();
    }

    private void Start()
    {
        movementInput.Mouse.MouseClick.performed += (_) => MouseClick();
        movementInput.Mouse.MouseClick.canceled += (context) => holdingMouseButton = false;
        movementInput.Keyboard.Movement.performed += (context) =>
        {
            lastActionWasClick = false;

            navMeshAgent.enabled = false;
            // navMeshAgent.ResetPath();
            movementVector = context.ReadValue<Vector2>();
        };
        movementInput.Keyboard.Movement.canceled += (_) =>
        {
            movementVector = Vector2.zero;
        };

        // destination = transform.position;
    }

    private void MouseClick()
    {
        if (!navMeshAgent.enabled) navMeshAgent.enabled = true;

        holdingMouseButton = true;
        lastActionWasClick = true;
        Vector2 mousePosition = movementInput.Mouse.MousePosition.ReadValue<Vector2>();

        Ray myRay = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
        {
            navMeshAgent.SetDestination(hitInfo.point);
        }


    }

    void FixedUpdate()
    {
        // WASD movement;
        if (holdingMouseButton)
        {
            MouseClick();
            return;
        }
        if (!lastActionWasClick)
        {
            Vector3 currentPos = rbody.position;

            Vector3 inputVector = new Vector3(movementVector.x, 0f, movementVector.y).normalized;

            Vector3 movement = inputVector * movementSpeed;
            Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
            // if (!lastActionWasClick) isoRenderer.SetDirection(movement);

            rbody.MovePosition(newPos);
            return;
        }


        // Mouse movement;
        if (lastActionWasClick && Vector3.Distance(rbody.position, navMeshAgent.destination) > 1f)
        {
            // isoRenderer.SetDirection((Vector2)destination - rbody.position);
            // Debug.Log(Vector3.Distance(rbody.position, navMeshAgent.destination));

            rbody.position = Vector3.MoveTowards(rbody.position, destination, movementSpeed * Time.deltaTime);

        }
        else if (lastActionWasClick)
        {
            lastActionWasClick = false;
            navMeshAgent.enabled = false;
        }
    }


}


using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private bool DEBUG_GODMODE = false;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 15f;

    [SerializeField] private InputManager inputManager;

    private Rigidbody rb;
    private BoxCollider boxCollider;
    private bool isAlive = true;

    public static Player Instance { get; private set; }
    public event EventHandler OnObstacleCollision;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Another player instance exist !");
            return;
        }
        Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        inputManager.OnJumpAction += OnJump;
    }


    private void OnJump(object sender, EventArgs e)
    {
        if (isAlive)
        {
            rb.linearVelocity += Vector3.up * (jumpForce * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Obstacle obstacle) && !DEBUG_GODMODE)
        {
            isAlive = false;
            OnObstacleCollision?.Invoke(this, EventArgs.Empty);
        }
    }
    void FixedUpdate()
    {
        if (isAlive)
        {
            HandleMovement();
            // HandleJetpack();
        }
    }
    // private void HandleJetpack()
    // {
    //     Vector3 halfExtends = boxCollider.bounds.extents;
    //     halfExtends.y = .025f;
    //     bool isOnGround = Physics.BoxCast(transform.position,
    //      halfExtends,
    //      Vector3.down,
    //      transform.rotation,
    //      boxCollider.bounds.extents.y,
    //      LayerMask.GetMask("floor"));
    // }
    private void HandleMovement()
    {
        Vector3 direction = new(inputManager.GetMovementHorizontal(), 0, 0);
        bool canMove = !Physics.BoxCast(transform.position,
            boxCollider.bounds.extents,
            direction,
            transform.rotation,
            speed * Time.fixedDeltaTime);
        if (canMove)
            rb.MovePosition(transform.position + speed * Time.fixedDeltaTime * direction);
    }
}

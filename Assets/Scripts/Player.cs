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
    public bool IsAlive { get; private set; } = true;

    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;

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
        if (IsAlive)
        {
            rb.linearVelocity += Vector3.up * (jumpForce * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!DEBUG_GODMODE)
        {
            if (collision.transform.TryGetComponent(out Obstacle obstacle))
            {
                Kill();
            }
            if (collision.transform.TryGetComponent(out Floor floor))
            {
                if (floor.CurrentState == Floor.FloorState.IsLava)
                {
                    Kill();
                }
            }
        }
    }
    public void Kill()
    {
        IsAlive = false;
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }
    void FixedUpdate()
    {
        if (IsAlive)
        {
            HandleMovement();
            CheckIfFloorIsLava();
        }
    }
    private void CheckIfFloorIsLava()
    {
        Vector3 halfExtends = boxCollider.bounds.extents;
        halfExtends.y = .025f;
        bool isOnGround = Physics.BoxCast(transform.position,
         halfExtends,
         Vector3.down,
         out RaycastHit hitInfo,
         transform.rotation,
         boxCollider.bounds.extents.y,
         LayerMask.GetMask("floor"));
        if (isOnGround && hitInfo.transform.TryGetComponent(out Floor floor))
        {
            if (floor.CurrentState == Floor.FloorState.IsLava)
            {
                Kill();
            }
        }
    }
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

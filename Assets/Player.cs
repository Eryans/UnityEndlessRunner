using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 15f;
    private GameInputs gameInputs;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private void Awake()
    {
        gameInputs = new();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        gameInputs.Player.Enable();
        moveAction = gameInputs.FindAction("Move");
        jumpAction = gameInputs.FindAction("Jump");
    }

    void FixedUpdate()
    {
        Vector3 direction = new(moveAction.ReadValue<float>(), 0, 0);
        bool canMove = !Physics.BoxCast(transform.position,
            boxCollider.bounds.extents,
            direction,
            transform.rotation,
            speed * Time.fixedDeltaTime);
        if (canMove)
            rb.MovePosition(transform.position + speed * Time.fixedDeltaTime * direction);

        if (jumpAction.IsPressed())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}

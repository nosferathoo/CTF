using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    public enum Phase
    {
        Normal = 0, HoldsFlag = 1, Dying = 2, Victory = 3
    }
    
    [SerializeField] private Transform playerCamera;
    [SerializeField] private LayerMask groundedLayerMask;
    [SerializeField] private float movementSpeed, mouseSensitivity, jumpForce;
    [SerializeField] [Tag] private string enemyTag, flagTag, baseTag;
    [SerializeField] private GameObject holdingFlag;

    private Phase _phase;
    private Rigidbody _rigidbody;
    private float _pitch, _yaw;

    public Phase PlayerPhase => _phase;

    private void Awake()
    {
        Assert.IsNotNull(playerCamera);
        Assert.IsNotNull(holdingFlag);
    }

    private void Start()
    {
        if (GameManager.Instance.CurrentPlayer)
        {
            Destroy(gameObject);
            return;
        }
        
        GameManager.Instance.CurrentPlayer = this;

        Cursor.lockState = CursorLockMode.Locked;
        
        _rigidbody = GetComponent<Rigidbody>();
        _yaw = transform.localEulerAngles.y;
        _pitch = 0;
        _phase = Phase.Normal;
        holdingFlag.SetActive(false);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        HandleMouse();

        if (transform.position.y < -50f)
        {
            StartCoroutine(Die());
        }
    }

    private void LateUpdate()
    {
        HandleMovement();
        HandleJumping();        
    }

    private void HandleMovement()
    {
        var v = _rigidbody.velocity;

        var movement = transform.TransformVector(new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"))) * movementSpeed;

        v.y = 0;
        v = Vector3.Lerp(v, movement, 5.0f * Time.deltaTime);
        v.y = _rigidbody.velocity.y;
        
        _rigidbody.velocity = v;
    }

    private void HandleMouse()
    {
        var delta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y")) * mouseSensitivity;

        _pitch = Mathf.Clamp(_pitch - delta.y, -90, 90);
        _yaw += delta.x;

        transform.localRotation = Quaternion.Euler(0, _yaw, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(_pitch,0,0);
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.1f, groundedLayerMask))
            {
                _rigidbody.AddForce(Vector3.up * jumpForce);
            }
        }
    }

    private IEnumerator Die()
    {
        _phase = Phase.Dying;
        enabled = false;
        UIManager.Instance.ShowDamage(true);
        yield return new WaitForSeconds(0.2f);
        UIManager.Instance.ShowDamage(false);
        GameManager.Instance.RestartLevel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(enemyTag))
        {
            StartCoroutine(Die());
        }

        if (_phase == Phase.HoldsFlag && collision.collider.CompareTag(baseTag))
        {
            _phase = Phase.Victory;
            enabled = false;
            GameManager.Instance.FinishGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(flagTag))
        {
            other.gameObject.SetActive(false);
            _phase = Phase.HoldsFlag;
            holdingFlag.SetActive(true);
        }
    }
}

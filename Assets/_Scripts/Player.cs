using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _velocity = Vector2.zero;

    private Rigidbody2D _rigidbody;
    private Camera _mainCam;
    
    [SerializeField] private float speed;
    [SerializeField] private float speedLimit;
    [SerializeField] private float slowdownCoefficient;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCam = Camera.main;
    }

    private void Update()
    {
//        Debug.Log($"{Input.GetAxisRaw("Horizontal Keyboard")} & {Input.GetAxisRaw("Horizontal Gamepad")} | {GameManager.Instance.inputType}");
        GameManager.Instance.movementAxis = new Vector2(
            GameManager.Instance.inputType == InputType.KeyboardMouse
                ? Input.GetAxisRaw("Horizontal Keyboard")
                : Input.GetAxisRaw("Horizontal Gamepad"),
            GameManager.Instance.inputType == InputType.KeyboardMouse
                ? Input.GetAxisRaw("Vertical Keyboard")
                : -Input.GetAxisRaw("Vertical Gamepad")
        );
        GameManager.Instance.movementAxis = GameManager.Instance.movementAxis.normalized;
        _velocity += GameManager.Instance.movementAxis * speed;
        _velocity = Vector2.ClampMagnitude(_velocity, speedLimit);
//        Debug.Log($"{GameManager.Instance.movementAxis} {_velocity}");
        _velocity *= slowdownCoefficient;
        
        _rigidbody.velocity = _velocity / Time.deltaTime;

        if (GameManager.Instance.inputType == InputType.KeyboardMouse)
        {
            Vector3 position = transform.position;
            Vector2 playerScreenPos = _mainCam.WorldToScreenPoint(new Vector3(position.x, position.y));
            GameManager.Instance.aimAxis = ((Vector2) Input.mousePosition - playerScreenPos).normalized;
        }
        else
            GameManager.Instance.aimAxis = new Vector2(Input.GetAxisRaw("Horizontal Shooting Gamepad"), -Input.GetAxisRaw("Vertical Shooting Gamepad")).normalized; 
        Debug.Log(GameManager.Instance.aimAxis);
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float health = 5f;
    public float strength = 1f;
    
    private Vector2 _velocity = Vector2.zero;

    private float _shootCountdown = 0f;
    private float _hitCountdown = 0f;
    
    private Rigidbody2D _rigidbody;
    private Camera _mainCam;

    [SerializeField] private float initialShootCountdown;
    
    [SerializeField] private float speed;
    [SerializeField] private float speedLimit;
    [SerializeField] private float slowdownCoefficient;

    [SerializeField] private float initialHitCountdown;
    
    [SerializeField] private Transform gun;
    [SerializeField] private Transform bulletSpawnPosition;

    public GameObject killParticles;
    
    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        
        // Velocity
        _velocity += GameManager.Instance.movementAxis * speed;
        _velocity = Vector2.ClampMagnitude(_velocity, speedLimit);
        _velocity *= slowdownCoefficient;
        
        _rigidbody.velocity = _velocity / Time.deltaTime;
    }

    private void Update()
    {
        // Input
        GameManager.Instance.movementAxis = new Vector2(
            GameManager.Instance.inputType == InputType.KeyboardMouse
                ? Input.GetAxisRaw("Horizontal Keyboard")
                : Input.GetAxisRaw("Horizontal Gamepad"),
            GameManager.Instance.inputType == InputType.KeyboardMouse
                ? Input.GetAxisRaw("Vertical Keyboard")
                : -Input.GetAxisRaw("Vertical Gamepad")
        );
        GameManager.Instance.movementAxis = GameManager.Instance.movementAxis.normalized;
//        Debug.Log(_hitCountdown);
        
        // Determine aimAxis
        if (GameManager.Instance.inputType == InputType.KeyboardMouse)
        {
            Vector3 position = transform.position;
            Vector2 playerScreenPos = _mainCam.WorldToScreenPoint(new Vector3(position.x, position.y));
            GameManager.Instance.aimAxis = ((Vector2) Input.mousePosition - playerScreenPos).normalized;
        }
        else
            GameManager.Instance.aimAxis = new Vector2(Input.GetAxisRaw("Horizontal Shooting Gamepad"), -Input.GetAxisRaw("Vertical Shooting Gamepad")).normalized; 
        
        // Gun rotation
        Vector3 originalRotation = Quaternion.LookRotation(GameManager.Instance.aimAxis + new Vector2(0, 0.0001f), Vector3.up).eulerAngles;
        
        if (GameManager.Instance.aimAxis != new Vector2(0, 0))
            gun.rotation = Quaternion.Euler(originalRotation.x, originalRotation.y > 0 ? originalRotation.y : -90, 0);
        
        // Shooting
        Vector2 realAimAxis = new Vector2(Input.GetAxisRaw("Horizontal Shooting Gamepad"), Input.GetAxisRaw("Vertical Shooting Gamepad"));

        _shootCountdown -= Time.deltaTime;
        _hitCountdown -= Time.deltaTime;
        
        bool shooting = GameManager.Instance.inputType == InputType.KeyboardMouse
            ? Input.GetMouseButton(0)
            : realAimAxis.magnitude > 0.1f;
        if (shooting && _shootCountdown <= 0f)
        {
            CameraController.Instance.Shake(GameManager.Instance.shootScreenShake);
            _shootCountdown = initialShootCountdown;
            GameObject instance = LevelManager.Instance.bulletQueue.Dequeue();
            LevelManager.Instance.bulletQueue.Enqueue(instance);
            instance.SetActive(false);
            instance.SetActive(true);
            instance.transform.position = bulletSpawnPosition.position;
            Vector3 originalInstanceRotation = Quaternion.LookRotation(GameManager.Instance.aimAxis, Vector3.up).eulerAngles;
            instance.transform.rotation = Quaternion.Euler(originalRotation.x, originalInstanceRotation.y > 0 ? originalInstanceRotation.y : -90, 0);//Quaternion.Euler(0, -90, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            Hit(other.gameObject.GetComponent<SimpleEnemy>().strength);
    }

    private void Hit(float enemyStrength)
    {
        if (_hitCountdown <= 0.1)
        {
            CameraController.Instance.Shake(GameManager.Instance.playerHitScreenShake);
            _hitCountdown = initialHitCountdown;
            health -= enemyStrength;
            if (health <= 0.1)
            {
                SceneManager.LoadScene("Scenes/GameOver");
            }
        }
        else
        {
//            Debug.Log("countdown");
        }
    }
}

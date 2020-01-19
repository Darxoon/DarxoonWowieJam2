using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleEnemy : MonoBehaviour
{

    public float health = 5;
    public float strength = 1;

    public float maxHealth = 5;
    public bool canDie = true;

    public bool onKillGoToLevel;
    [SerializeField] private string killDestination;

    [SerializeField] private bool usesPlayerAimAxis = false;
    
    [Header("Calculations")]
    
    [SerializeField] private float turnSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float direction;
    [SerializeField] private float minShootDistance;
    [SerializeField] private float initialShootCountdown;

    [Header("Components")] 
    
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private SpriteRenderer eyeSpriteRenderer;

    [Header("Gun")] 
    
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Transform bulletSpawnPointTransform;

    private bool _livingAgain = true;
    private float _hitCountdown;
    private float _shootCountdown = 0f;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 unitVelocity = (Player.Instance.transform.position - transform.position).normalized;
        Quaternion tempDirection = Quaternion.LookRotation(unitVelocity.x > 0 ? -unitVelocity : unitVelocity, Vector3.up);
        float rawDirection = tempDirection.eulerAngles.x + (unitVelocity.x > 0 ? 180f : 0f);
        direction = Mathf.LerpAngle(direction, rawDirection, turnSpeed);

        Transform transform1;
        (transform1 = transform).rotation = Quaternion.Euler(0, 0, direction);
        if(gunTransform)
            gunTransform.rotation = Quaternion.Euler(0, 0, rawDirection);
//        transform1.position += Time.deltaTime * -speed * transform1.right.normalized;
        rigidbody.velocity = -speed * transform1.right.normalized;

        _hitCountdown -= Time.deltaTime;
        if (_hitCountdown <= 0.1 && !_livingAgain)
        {
            gameObject.tag = "Enemy";
            eyeSpriteRenderer.enabled = true;
            _livingAgain = true;
            Debug.Log("Living again!");
        }

        if (gunTransform)
        {
            // Shooting
            _shootCountdown -= Time.deltaTime;
            if (_shootCountdown <= 0.1f)
            {
                Debug.Log("shooot");
                _shootCountdown = initialShootCountdown;
//                CameraController.Instance.Shake(GameManager.Instance.shootScreenShake);
                
                GameObject instance = LevelManager.Instance.bulletQueue.Dequeue();
                LevelManager.Instance.bulletQueue.Enqueue(instance);
                
                instance.SetActive(false);
                instance.SetActive(true);
                instance.transform.position = bulletSpawnPointTransform.position;

                Vector3 originalInstanceRotation = Quaternion.LookRotation(usesPlayerAimAxis ? GameManager.Instance.aimAxis : unitVelocity, Vector3.up).eulerAngles;
                instance.transform.rotation = Quaternion.Euler(gunTransform.rotation.eulerAngles.z, originalInstanceRotation.y > 0 ? originalInstanceRotation.y : -90, 0);//Quaternion.Euler(0, -90, 0);

            }
        }
    }

    public bool Hit(float playerStrength)
    {
        if(_hitCountdown >= 0.1f)
            return false;
        health -= playerStrength;
        _hitCountdown = .3f;
        if (health <= 0.2f)
        {
            if (onKillGoToLevel)
                SceneManager.LoadScene(killDestination);
            gameObject.tag = "InactiveEnemy";
            if(eyeSpriteRenderer)
                eyeSpriteRenderer.enabled = false;
            _livingAgain = false;
            CameraController.Instance.Shake(GameManager.Instance.killScreenShake);
            if(particleSystem)
                particleSystem.Play();
            if (canDie)
                Destroy(gameObject);
            else
                health = maxHealth;

            return true;
        } 
        else
            return false;
    }

}

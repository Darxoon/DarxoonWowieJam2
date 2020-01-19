using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleEnemy : MonoBehaviour
{

    public float health = 5;
    public float strength = 1;

    public float maxHealth = 5;
    public bool canDie = true;

    public bool onKillGoToLevel = false;
    [SerializeField] private string killDestination;
    
    [Header("Calculations")]
    
    [SerializeField] private float turnSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float direction;

    [Header("Components")] 
    
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private SpriteRenderer eyeSpriteRenderer;

    private bool _livingAgain = true;
    private float _hitCountdown = 0f;
    
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

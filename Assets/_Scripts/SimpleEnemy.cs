using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{

    public float health = 5;
    public float strength = 1;

    public float maxHealth = 5;
    public bool canDie = true;
    
    [Header("Calculations")]
    
    [SerializeField] private float turnSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float direction;

    [Header("Components")] 
    
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private ParticleSystem particleSystem;

    
    private float _dieCountdown = 0f;
    
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

        _dieCountdown -= Time.deltaTime;
    }

    public void Hit(float playerStrength)
    {
        if(_dieCountdown >= 0.1f)
            return;
        health -= playerStrength;
        if (health <= 0.2f)
        {
            _dieCountdown = 6f;
            CameraController.Instance.Shake(GameManager.Instance.killScreenShake);
            if (canDie)
                Destroy(gameObject);
            else
            {
                
                health = maxHealth;
            }
        }
    }

}

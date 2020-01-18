using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health = 5;
    

    [Header("Calculations")]
    
    [SerializeField] private float turnSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float direction;

    [SerializeField] private float explosionStrength;
    
    [Header("References")]
    
    [SerializeField] private Player player;
    [SerializeField] private CircleCollider2D killCollider;

    [Header("Components")] 
    
    [SerializeField] private new Rigidbody2D rigidbody;

    private Collider2D[] contacts = new Collider2D[50];
    
    private void Start()
    {
        player = FindObjectOfType<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 unitVelocity = (player.transform.position - transform.position).normalized;
        Quaternion tempDirection = Quaternion.LookRotation(unitVelocity.x > 0 ? -unitVelocity : unitVelocity, Vector3.up);
        float rawDirection = tempDirection.eulerAngles.x + (unitVelocity.x > 0 ? 180f : 0f);
        direction = Mathf.LerpAngle(direction, rawDirection, turnSpeed);

        Transform transform1;
        (transform1 = transform).rotation = Quaternion.Euler(0, 0, direction);
//        transform1.position += Time.deltaTime * -speed * transform1.right.normalized;
        rigidbody.velocity = -speed * transform1.right.normalized;
    }

    public void Hit(float strength)
    {
        health -= strength;
        if (health <= 0.2f)
            Destroy(gameObject);
    }

}

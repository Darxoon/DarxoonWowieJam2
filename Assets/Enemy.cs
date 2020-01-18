using UnityEngine;

public class Enemy : MonoBehaviour
{
    

    [Header("Calculations")]
    
    [SerializeField] private float turnSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float direction;
    
    [Header("References")]
    
    [SerializeField] private Player player;

    [Header("Components")] 
    
    [SerializeField] private Rigidbody2D rigidbody;
    
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
}

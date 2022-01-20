using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _aimAxis;
    
    private Rigidbody2D _rigidbody;

    private float _strength;

    public BulletOrigin origin = BulletOrigin.Player;
    public SimpleEnemy enemyOrigin;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _strength = 0;
        _aimAxis = Vector2.zero;
        Invoke(nameof(SetAimAxis), 0.0f);
    }

    private void SetAimAxis()
    {
        _strength = origin == BulletOrigin.Player ? Player.Instance.strength : enemyOrigin.strength;
        _aimAxis = origin == BulletOrigin.Player
            ? GameManager.Instance.aimAxis
            : enemyOrigin.gunAxis;
    }

    private void Update()
    {
        if (GameManager.Instance.usingGlobalBulletDirection && GameManager.Instance.inputType == InputType.KeyboardMouse && Input.GetMouseButton(0)
            || GameManager.Instance.usingGlobalBulletDirection && GameManager.Instance.inputType == InputType.Gamepad && GameManager.Instance.aimAxis.magnitude > 0.1
            || !GameManager.Instance.usingGlobalBulletDirection)
            _rigidbody.velocity = (GameManager.Instance.usingGlobalBulletDirection ? GameManager.Instance.aimAxis : _aimAxis) * GameManager.Instance.bulletSpeed;
        else if (GameManager.Instance.usingGlobalBulletDirection)
            _rigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("BulletBorders"))
            SetActive(false);
        if (other.gameObject.CompareTag("Enemy") && origin == BulletOrigin.Player)
        {
            CameraController.Instance.Shake(GameManager.Instance.hitScreenShake);
            if (other.gameObject.GetComponent<SimpleEnemy>().Hit(_strength))
            {
//                Debug.Log("hi");
                Transform transform1 = transform;
                Instantiate(Player.Instance.killParticles, transform1.position + new Vector3(0, 0, -5), transform1.rotation);
            }

            SetActive(false);
        }

        if (other.gameObject.CompareTag("Player") && origin == BulletOrigin.Enemy)
        {
            Debug.Log($"Player hit with {_strength}", this);
            CameraController.Instance.Shake(GameManager.Instance.playerHitScreenShake);
            Player.Instance.Hit(_strength);

            SetActive(false);
        }
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            SetActive(false);
            gameObject.SetActive(true);
        }
        else
        {
            origin = BulletOrigin.Player;
            gameObject.SetActive(false);
        }
    }

}

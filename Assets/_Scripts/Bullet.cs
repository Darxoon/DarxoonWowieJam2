using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Collider2D _mapCollider;

    private Collider2D _collider;

    private Vector2 _aimAxis;
    
    private Rigidbody2D _rigidbody;

    private Collider2D[] _contacts = new Collider2D[20];

    private float _strength = 0;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        _mapCollider = LevelManager.Instance.mapCollider;
    }

    private void OnEnable()
    {
        _strength = Player.Instance.strength;
        _aimAxis = GameManager.Instance.aimAxis;
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
            gameObject.SetActive(false);
        if (other.gameObject.CompareTag("Enemy"))
        {
            CameraController.Instance.Shake(GameManager.Instance.hitScreenShake);
            other.gameObject.GetComponent<SimpleEnemy>().Hit(_strength);
                
            gameObject.SetActive(false);
        }
    }

}

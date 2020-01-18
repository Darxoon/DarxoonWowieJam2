using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Collider2D _mapCollider;

    private Collider2D _collider;

    private Vector2 _aimAxis;
    
    private Rigidbody2D _rigidbody;

    private Collider2D[] _contacts = new Collider2D[20];
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        _mapCollider = GameManager.Instance.mapCollider;
    }

    private void OnEnable()
    {
        
        _aimAxis = GameManager.Instance.aimAxis;
    }

    private void Update()
    {
        _rigidbody.velocity = _aimAxis * GameManager.Instance.bulletSpeed;
//        if (_collider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
//        {
//            ContactFilter2D filter = new ContactFilter2D {layerMask = LayerMask.GetMask("Enemy")};
//            _collider.GetContacts(filter, _contacts);
//
//            foreach (Collider2D contact in _contacts)
//            {
//                Debug.Log("killled broo", contact);
//            }
//        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Map"))
            gameObject.SetActive(false);
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().Hit(1);
                
            gameObject.SetActive(false);
        }
    }

}

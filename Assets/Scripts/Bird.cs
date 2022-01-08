using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchPower = 500;
    [SerializeField] float _maxDistance = 5;

    Vector2 _startingPosition;
    Rigidbody2D _rigidbody2d;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _startingPosition = transform.position;
        _rigidbody2d.isKinematic = true;
    }

    void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2d.position;
        Vector2 direction = _startingPosition - currentPosition;
        direction.Normalize();

        _rigidbody2d.isKinematic = false;
        _rigidbody2d.AddForce(direction * _launchPower);

        _spriteRenderer.color = Color.white;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;
     
        float distance = Vector2.Distance(desiredPosition, _startingPosition);

        if (distance > _maxDistance)
        {
            Vector2 direction = desiredPosition - _startingPosition;
            direction.Normalize();
            desiredPosition = _startingPosition + (direction * _maxDistance);
        }

        if (desiredPosition.x > _startingPosition.x)
            desiredPosition.x = _startingPosition.x;

        _rigidbody2d.position = desiredPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetBird());
    }

    IEnumerator ResetBird()
    {
        yield return new WaitForSeconds(3);
        transform.position = _startingPosition;
        _rigidbody2d.isKinematic = true;
        _rigidbody2d.velocity = Vector2.zero;
    }
}

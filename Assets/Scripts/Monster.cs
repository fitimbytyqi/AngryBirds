using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;

    bool _hasDied;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
       
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        return bird != null || collision.contacts[0].normal.y < -0.5;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(2);
         gameObject.SetActive(false);
    }
}

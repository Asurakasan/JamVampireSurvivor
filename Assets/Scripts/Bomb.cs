using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Speed;
    public float ExplodeTime;
    public LayerMask layerToHit;
    public float FieldOfImpact;
    public float Force;
    private float timer;
    private Vector3 _direction;
    public int damage;

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * Speed * Time.deltaTime;
        timer += Time.deltaTime;
        if(timer > ExplodeTime)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, FieldOfImpact, layerToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * Force);
            obj.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}

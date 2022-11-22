using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Life")]
    public float MaxLife;
    public float CurrentLife;

    [Header("Knockback")]
    public float knockbackDuration;
    public float knockbackPower;

    [Header("Movement")]
    public float Speed = 4;

    [Header("Damage")]
    public float Damage = 1;

    private GameObject _player;
    private Rigidbody2D _rb;
    private CandyDrop candyDrop;
    public GameObject fx;
    public GameObject bloodFX;
    public GameObject deadFx;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        candyDrop = GetComponent<CandyDrop>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentLife = MaxLife;
    }

    public void Initialize(GameObject player)
    {
        _player = player;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        direction.z = 0;

        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            _rb.velocity = direction * Speed;

        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == _player)
        {
            StartCoroutine(_player.GetComponent<PlayerController>().Knockback(knockbackDuration, knockbackPower, transform));
            _player.GetComponent<PlayerController>().TakeDamage(Damage);
        }
    }
    public void TakeDamage(float damage)
    {
        CurrentLife -= damage;
        if (CurrentLife <= 0)
        {
           candyDrop.DropCandy();
           MainGameplay.Instance.Enemies.Remove(this);
           GameObject DeadFX = Instantiate(deadFx, transform.position, Quaternion.identity);
           Destroy(DeadFX, 3);
           Destroy(gameObject);
        }
        GameObject FX = Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(FX, 1);
        GameObject blood = Instantiate(bloodFX, transform.position, Quaternion.identity);
        Destroy(blood, 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {
    [SerializeField] private float speed;
    [SerializeField] private float attackRate;

    private Vector2 target;
    private Animator _animator;
    private float step;
    private bool GoingToPlayer = true; //make state machine later
    private float _currentAttackRateTime;


    private void Awake() {
        _animator = GetComponent<Animator>();
        _currentAttackRateTime = 0f;
    }

    private void Start() {
        target = new Vector2(0f, transform.position.y);
        SetCurrentHealth();
    }

    private void Update() {
        if (GoingToPlayer) {
            Walk();
        } else if(!Dying) {
            Attack();
        }
    }

    private void Walk() {
        step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void Attack() {

        if(_currentAttackRateTime <= 0f) {
            _currentAttackRateTime = attackRate;
            _animator.SetTrigger("Attack");
        }
        _currentAttackRateTime -= Time.deltaTime;
    }

    public override void SetHitAnimation() {
        _animator.SetTrigger("Hit");
    }

    public override void SetDieAnimation() {
        _animator.SetTrigger("Die");
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "CatGirl") {
            GoingToPlayer = false;
            _animator.SetBool("IsWalking", false);
        }
    }
}

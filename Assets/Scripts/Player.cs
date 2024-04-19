using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private Animator _animator;


    private void Awake() {
        _animator = GetComponent<Animator>(); 
    }

    private void Start() {
        SetCurrentHealth();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            transform.localScale = new Vector3(-0.5f, 0.5f);
            if(!ShouldBeDamaging) {
                Attack();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            transform.localScale = new Vector3(0.5f, 0.5f);
            if (!ShouldBeDamaging) {
                Attack();
            }
        }

    }

    private void Attack() {
        _animator.SetTrigger("Attack");

    }

    public override void SetHitAnimation() {
        _animator.SetTrigger("Hit");
    }
    public override void SetDieAnimation() {
        _animator.SetTrigger("Die");
    }

}

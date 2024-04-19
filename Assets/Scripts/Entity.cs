using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable {
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private float maxHealth = 3f;

    private List<IDamageable> iDamageables = new List<IDamageable>();
    public bool ShouldBeDamaging { get; private set; } = false;
    private RaycastHit2D[] hits;
    private float currentHealth;
    public bool Dying = false;

    public void SetCurrentHealth() {
        currentHealth = maxHealth;
    }
    public IEnumerator DamageWhileAnimationIsActive() {
        ShouldBeDamaging = true;
        while (ShouldBeDamaging) {
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);
            for (int i = 0; i < hits.Length; i++) {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
                if (iDamageable != null && !iDamageables.Contains(iDamageable)) {
                    iDamageable.Damage(damageAmount);
                    iDamageables.Add(iDamageable);
                }
            }

            yield return null;
        }
        ResetDamageables();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
    public void Damage(float damageAmount) {
        if (Dying) return;
        if (currentHealth <= 0) {
            Die();
        } else {
            ShouldBeDamagingToFalse();
            SetHitAnimation();
            currentHealth -= damageAmount;
        }
    }

    public virtual void SetHitAnimation() { }
    public virtual void SetDieAnimation() { }

    private void Die() {
        Dying = true;
        SetDieAnimation();
        Destroy(gameObject,2f);
    }
    private void ResetDamageables() {
        iDamageables.Clear();
    }

    #region Animation Triggers

    public void ShouldBeDamagingToTrue() {
        ShouldBeDamaging = true;
    }

    public void ShouldBeDamagingToFalse() {
        ShouldBeDamaging = false;
    }

    #endregion
}

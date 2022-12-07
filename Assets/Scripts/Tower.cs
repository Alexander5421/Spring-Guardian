using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
//TODO: flip the tower if the enemy is on the other side of the tower (if the enemy is on the left side of the tower, flip the tower to the left)
[RequireComponent(typeof(CircleCollider2D))]
public class Tower : MonoBehaviour
{
    public List<Enemy> potentialTargets = new List<Enemy>();
    public CircleCollider2D rangeCollider;
    public Animator animator;
    public float coolDown = 1f;
    public float coolDownTimer = 0f;
    public Projectile projectilePrefab;
    public Transform firePoint;
    public float buildCooldown = 5f;
    public Transform rangeIndicator;

    public void FixedUpdate()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        // if there are no targets, do nothing
        if (potentialTargets.Count == 0)
        {
            animator.SetBool("IsAttack",false);
            return;
        }
        // if the cooldown timer is not 0, reduce it by the time since the last frame
        animator.SetBool("IsAttack",true);
        // if the cooldown timer is 0, shoot a projectile at the the target has the largest progress
        if (coolDownTimer <= 0)
        {
            coolDownTimer = coolDown;
            if (potentialTargets.Count()== 0){
                animator.SetBool("IsAttack",false);
                return;
            }
            Enemy target = potentialTargets[0];
            float maxProgress = 0;
            if (potentialTargets.Count()== 0){
                animator.SetBool("IsAttack",false);
                return;
            }
            try{
                foreach (var enemy in potentialTargets)
                {
                    if (!(enemy.Progress > maxProgress)) continue;
                    target = enemy;
                    maxProgress = enemy.Progress;
                }
            }
            catch (MissingReferenceException){
                animator.SetBool("IsAttack",false);
                return;
            }
            catch (NullReferenceException){
                animator.SetBool("IsAttack",false);
                return;
            }

            // instantiate the projectile at the fire point
            // direction is the vector from the fire point to the target
            // only the x and y components are used
            Projectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            try{
                var direction = target.transform.position - firePoint.position;
                direction.z = 0;
                if (direction.x <= 0){
                    animator.SetBool("IsRight", false);
                }
                if (direction.x > 0){
                    animator.SetBool("IsRight", true);
                }
                //set the rotation of the projectile to the direction
                projectile.transform.right = direction;
            }
            catch (MissingReferenceException){
                animator.SetBool("IsAttack",false);
                return;
            }
            catch (NullReferenceException){
                animator.SetBool("IsAttack",false);
                return;
            }
            
            try {
                projectile.target = target;
            }
            catch (MissingReferenceException){
                animator.SetBool("IsAttack",false);
            }
            catch (NullReferenceException){
                animator.SetBool("IsAttack",false);
            }
                
        }
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.DrawWireSphere(transform.position, rangeCollider.radius);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            potentialTargets.Add(other.GetComponent<Enemy>());
            other.gameObject.GetComponent<Enemy>().OnQuit+= RemoveTarget;
        }
        
       
    }

    private void RemoveTarget(Enemy target)
    {
        // test if the target is in the list
        if (potentialTargets.Contains(target))
        {
            // remove the target from the list
            potentialTargets.Remove(target);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            potentialTargets.Remove(other.GetComponent<Enemy>());
            other.gameObject.GetComponent<Enemy>().OnQuit-= RemoveTarget;
        }
    }

    public void ShowRange()
    {
        if (rangeIndicator==null)
        {
            Debug.LogWarning($"{gameObject.name} has not assigned a indicator");
        }

        var radius = rangeCollider.radius;
        rangeIndicator.transform.localScale = new Vector3(radius * 2, radius * 2, 1f);
        rangeIndicator.gameObject.SetActive(true);
    }
    
    public void HideRange()
    {
        rangeIndicator.gameObject.SetActive(false);
    }
}
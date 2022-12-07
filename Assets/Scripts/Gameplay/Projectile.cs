using System;
using UnityEngine;

//TODO still not distinguishing between the death caused by this projectile and the target loss. 
public class Projectile : MonoBehaviour
{
    public int dmg = 1;
    public Enemy target;
    public float speed = 10f;
    
    public void Start()
    {
        try{
            if (target.isDead)
            {
                Destroy(gameObject);
                return;
            }
            target.OnDeath += (targetOnOnDeath);
        }
        catch (MissingReferenceException){
            Destroy(gameObject);
            return;
        }
        catch (NullReferenceException){
            Destroy(gameObject);
            return;
        }

    }

    protected void targetOnOnDeath(Enemy _)
    {
        TargetIsDead();
    }

    public virtual void FixedUpdate()
    {
        try {
            // Move towards target
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            
            // check distance to target
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                // hit target
                target.OnDeath -= (targetOnOnDeath);
                Destroy(gameObject);
            target.TakeDamage(dmg);
            }
        }
        catch (MissingReferenceException){
            Destroy(gameObject);
            return;
        }
        catch (NullReferenceException){
            Destroy(gameObject);
            return;
        }

    }
    
    

    private void TargetIsDead()
    {
        // check whether the gameobject is still alive
        
        Destroy(gameObject);
    }

   
}
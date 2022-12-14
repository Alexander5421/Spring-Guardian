using System.Collections.Generic;
using UnityEngine;

class ExplosiveProjectile : Projectile
{
    public float range = 2f;
    public float aoeCoeff = 1.5f;
    public override void FixedUpdate()
    {
        // Move towards target
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        // check distance to target
        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            // hit target
            
            // hit nearby enemies
            var enemies = new List<Enemy>(GameData.Instance.spawnManager.existingEnemies);
            target.OnQuit -= (base.targetOnOnDeath);
            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < range)
                {
                    // dmg should be based on distance from center of explosion
                    // int affectedDmg=(int)Mathf.Pow(1-(Vector3.Distance(transform.position, enemy.transform.position)/range), aoeCoeff)*dmg;
                    enemy.TakeDamage(dmg);
                }
            }
            Destroy(gameObject);
        }
    }
}
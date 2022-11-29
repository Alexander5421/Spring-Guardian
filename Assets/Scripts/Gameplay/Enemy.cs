using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PathCreator pathCreator;

    public float speed = 5;

    // damage caused to player if the enemy reach the capture point
    public int point = 1;
    
    public HealthBar healthBar;

    public Animator animator;
    
    public int reward = 10;

    private int health =1;
    
    public int MaxHealth = 100;
    
    private bool isDead = false;
    
    // event OnDeath
    public event Action<Enemy> OnDeath;

    // enemy quit from the game
    public event Action<Enemy> OnQuit;

    private float distanceSofar = 0;
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    public float Progress
    {
        get { return distanceSofar / pathCreator.path.length; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (pathCreator == null)
        {
            Debug.LogError($"{gameObject.name} does not have a path to follow");
        }
        animator = GetComponent<Animator>();
        health = MaxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        // update health bar
        healthBar.healthRatio = (float) health / MaxHealth;
        
        if (health <= 0)
        {
            // log
            healthBar.healthRatio = 0;
            OnDeath?.Invoke(this);
            OnQuit?.Invoke(this);
            GameData.Instance.playerData.Money += reward;
            animator.SetBool(IsDie, true);
            isDead = true;
            // Destroy the enemy after the animation is finished

        }
    }
    // will call by animation event
    public void OnDeathFinish()
    {
        Destroy(gameObject, 0.5f);
    }

    public void ReachEnd()
    {
        OnQuit?.Invoke(this);
    }


    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        distanceSofar += speed * Time.fixedDeltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceSofar,EndOfPathInstruction.Stop);
    }
    
    // show enemy health bar use gizmos
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3(1, 0.1f, 1));
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3((float)health / MaxHealth, 0.1f, 1));
    // }
    
}

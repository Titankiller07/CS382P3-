using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;
    public float powerUpDropChance = 1f;
    protected bool calledShipDestroyed = false;
    protected BoundsCheck bndCheck;


    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 pos
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
     Move();
     if(bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown))
     {
         Destroy(gameObject);
     }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    void OnCollisionEnter(Collision coll){
        GameObject otherGO = coll.gameObject;
        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        if(p != null){
            if(bndCheck.isOnScreen){
                health -= Main.GET_WEAPON_DEFINTION(p.type).damageOnHit;
                if(health <= 0){
                    if(!calledShipDestroyed){
                        calledShipDestroyed = true;
                        Main.SHIP_DESTROYED(this);
                    }
                    Destroy(gameObject);
                    ScoreManager.Instance.AddScore(score);
                }
            }
            Destroy(otherGO);
        }
        else {
            print("Enemy hit by non-hero projectile: " + otherGO.name);
            }
    }
    
    public void TakeDamage(float damageAmount)
    {
        if (bndCheck.isOnScreen)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                if (!calledShipDestroyed)
                {
                    calledShipDestroyed = true;
                    Main.SHIP_DESTROYED(this);
                }
                Destroy(gameObject);
            }
        }
    }

    // Handle continuous damage from trigger-based weapons like laser
    void OnTriggerStay(Collider other)
    {
        ProjectileHero p = other.GetComponent<ProjectileHero>();
        if (p != null && p.type == eWeaponType.laser)
        {
            // Apply damage per second scaled by deltaTime
            float damagePerFrame = Main.GET_WEAPON_DEFINTION(p.type).damagePerSec * Time.deltaTime;
            TakeDamage(damagePerFrame);
        }
    }
}

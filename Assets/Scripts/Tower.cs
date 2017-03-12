using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Tower : MonoBehaviour {

    public float range;
    public float damage;
    public float projectileSpeed;
    public float power;
    public float coolnownTime;
    private float cooldown;
    public GameObject turretHead;
    public SphereCollider sphCol;
    public Transform currentEnemy;
    public Transform firePoint;
    public List<Transform> enemies = new List<Transform>();

    private void Awake()
    {
        sphCol = this.gameObject.AddComponent<SphereCollider>();
       
        

    }
    // Use this for initialization
    void Start () {
        sphCol.isTrigger = true;
        sphCol.radius = range;
        cooldown = coolnownTime;


    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            Debug.Log("registring a new enemy " + other.gameObject.name);
            enemies.Add(other.gameObject.transform);
            currentEnemy = Utilites.GetClosestObjectTo(enemies, transform);

        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            enemies.Remove(other.gameObject.transform);
        }
        if (enemies.Count == 0)
        {
            currentEnemy = null;
        }
    }



    private void LookAtClosest(Transform trans)
    {
        turretHead.transform.LookAt(trans);
    }
    private void Fire(Transform enemy)
    {
       
        if (enemy)
        {
            Debug.Log("firing at enemy " + enemy.name);
            GameObject bullet = Resources.Load("bullet") as GameObject;
            var projectile = Instantiate(bullet, firePoint.position, Quaternion.identity);
            bullet.transform.parent = null;
            var proj = projectile.AddComponent<Projectile>();
            proj.firePoint = this.firePoint;
            proj.damage = this.damage;
            proj.speed = this.projectileSpeed;
            proj.range = this.range;
            proj.power = this.power;

        }
        
    }

    // Update is called once per frame
    void Update () {
        LookAtClosest(currentEnemy);
        cooldown -= Time.deltaTime;
        if (cooldown < 0 && currentEnemy != null)
        {
            Fire(currentEnemy);
            cooldown = coolnownTime;
        }

       
    }
}

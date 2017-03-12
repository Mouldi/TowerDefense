using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    public float range;
    public float damage;
    public float speed;
    public float power;
    public Transform firePoint;
    public Vector3 initPos;
    public Rigidbody rig;

    private void Awake()
    {

        
        rig = gameObject.GetComponent<Rigidbody>();

    }

    // Use this for initialization
    void Start () {
        rig.useGravity = true;
        initPos = firePoint.up;
       // rig.AddForce(initPos , ForceMode.Impulse);



    }
	
	// Update is called once per frame
	void Update () {
        range -= Time.deltaTime;
        if (range < 0)
        {
            Destroy(this.gameObject);

        }
        this.transform.Translate(initPos * speed * Time.deltaTime, Space.Self);

		
	}
    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {


    public float speed;
    public GameObject path  ;
    public List<Transform> nodes = new List<Transform>();
    public Transform currentNode;
    public Rigidbody rig;
    public int index;
    public float health;



    void Awake()
    {

        rig = this.gameObject.AddComponent<Rigidbody>();
        GetThePath();
    }

    // Use this for initialization
    void Start () {
        rig.isKinematic = true;
       

        PopulatePath();

    }
    public void TakeDamage(float damage)
    {
        Debug.Log("taking damage from turret by - " + damage);
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    void GetThePath()
    {
        if (!path)
        {

            path = GameObject.FindGameObjectWithTag("Path") as GameObject;

        }

    }

    void PopulatePath()
    {
        nodes = path.GetComponentsInChildren<Transform>().Where(child => child.gameObject != path.gameObject).ToList(); 
    }

   
   
    private void MoveToNextNode()
    {
        var dist = Utilites.GetDistance(this.transform.position, currentNode.position);
        if (dist < 2)
        {

            index++;
            if (index == nodes.Count)
            {
                index = 0;
            }
            currentNode = nodes[index];
        }
    }

    void MoveToNode(Transform trans)
    {
        transform.Translate(trans.forward * Time.deltaTime * speed, Space.Self);
        transform.LookAt(trans );
    }
    void NavigatePath()
    {

       currentNode = nodes[index];
        MoveToNode(currentNode);
        MoveToNextNode();

    }
	
	// Update is called once per frame
	void Update () {
        NavigatePath();
        
       
       
    }
}

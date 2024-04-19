using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Item { acorn,
    airplane,
    ant,
    apple,
    backpack,
    bag,
    ball,
    balloon,
    banana,
    basket,
    bee,
    bicycle,
    bone,
    book,
    book2,
    bow,
    broom,
    butterfly,
    cake,
    can
};

[RequireComponent(typeof(Rigidbody))]
public class Pickable : MonoBehaviour
{   

    public Item type;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }   

    // Update is called once per frame
    public void DisableRigidBody()
    {
        rb.isKinematic = true;
    }
}

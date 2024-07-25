using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInRigidbody : MonoBehaviour
{
    ConstantForce _constantForce;
    // Start is called before the first frame update
    void Awake()
    {
        _constantForce = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    public void SetConstantForce(Vector3 value)
    {
        _constantForce.force = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* @author Charles Lemaire, 2021
* @date - 2021-08-04
* 
* @Github https://github.com/TheGreatPixelman
*/

public enum SimulationType { AnimationCurve, DragEquation, }

[RequireComponent(typeof(Rigidbody))]
public class KinematicTerminalVelocity : MonoBehaviour
{
    //Public Variables
    public Vector3 gravity = Physics.gravity;

    public float terminalVelocity = 50f;
    public AnimationCurve dragCurve = AnimationCurve.Linear(0,0,1,1);
    public SimulationType simulationType;
    public Vector3 velocity;
    public float CD;
    public float mass = 1f;

    //Private Variables
    [SerializeField] Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (simulationType)
        {
            case SimulationType.AnimationCurve:
                ApplyForcesWithAnimationCurve();
                break;
            case SimulationType.DragEquation:
                ApplyForcesDragEquation();
                break;
        }
        
    }

    /// <summary>
    /// Simulation based on an animation Curve
    /// This solution is more arcady but easier to tweak by hand
    /// Changing the value to a lower velocity midSimulation won't work. (To Be Fixed)
    /// </summary>
    void ApplyForcesWithAnimationCurve()
    {
        float dragRatio = 1-dragCurve.Evaluate(Mathf.Abs(velocity.y) / terminalVelocity);

        velocity += gravity * Time.fixedDeltaTime * dragRatio * mass;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Simulation based on the Drag Equation
    /// The Drag Coefficient (CD) is calculated automatically with a given Terminal Velocity
    /// This solution is realistic but harder to tweak manually
    /// </summary>
    void ApplyForcesDragEquation()
    {
        //Safeguard against the division by 0
        CD = 6.245247f / (terminalVelocity > 0 ? terminalVelocity : 0.001f);

        float areaSphere = Mathf.Pow(transform.localScale.x,2) * Mathf.PI;
        Vector3 drag = CD * 0.5f * 1 * velocity * areaSphere;
        Debug.Log(drag.ToString());

        velocity += (gravity - drag) * Time.fixedDeltaTime * mass;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}

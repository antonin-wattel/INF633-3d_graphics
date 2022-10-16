using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepper : MonoBehaviour
{
    [Header("Stepper Settings")]
    public Transform homeTransform; // The position and rotation from which we want to stay in range (represented as the blue chip).
    public float distanceThreshold = 0.4f; // If we exceed this distance threshold, we come back to the home position.
    public float angleThreshold = 135f; // If we exceed this rotation threshold, we come back to the home rotation.
    public float moveDuration; // The time it takes a step to complete.
    [Range(0, 1)]
    public float stepOvershootFraction; // To add some variability, we add a fraction to overshoot the target.

    [Header("Raycast Settings")]
    public LayerMask groundRaycastMask = ~0; // Ground layer that you need to detect by raycasting.
    public float heightOffset; // Necessary when foot joints aren't exactly at the base of the foot geometry.

    // Flag to define when a leg is moving.
    public bool Moving;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // We put the steppers at the top of the hierarchy, to avoid other influences from the parent transforms and to see them better.
        // Transform new_tranform = Instantiate(transform);
        // new_tranform.parent = null;
        
        // transform.SetParent(null);
        // create a duplicate of transform (same but different name and set it with no parents)
        // Transform new_transform = Instantiate(transform);    
        // set the new transform as the parent of the original transform
        // new_transform.SetParent(null);
        // delete og game object of transform
        // Destroy(transform.gameObject);

        // transform = 
        //unpack the transform
        transform.SetParent(null);

        //duplicate the transform game object
        //now set the transform game object as the new one
        
        




        // transform.SetParent(transform.parent.parent.parent.parent);//pb this does not move and IK is still shit
        // transform.SetParent(homeTransform.parent);

        // idea: set a common parent to transform.parent.parent and transform

        //create new parent gameobject
        // GameObject newParent = new GameObject("CommonParent");

        // PROBLEM: THIS IS NOT COOL WHEN WE WANT TO HAVE MANY QUADRUPEDS
        // transform.SetParent(transform.parent.parent); //-> not enough (still fucks up the IK :/)
        // hack: just change the name of 
        // if find a different game object in the scene that shares the same name, change the name
        
        // print("transform.name: "+transform.name);
        
        // //make a copy of the name

        // string name_copy = transform.name;
        // transform.name = name_copy + "_";

        // // if we find a game object with the same name, change the name in the hierarchy
        // if (GameObject.Find(name_copy) != null)
        // {
        //     print("found a game object with the same name: change name");
        //     int i=0;
        //     while (GameObject.Find(name_copy) != null){
        //         name_copy = transform.name + i;
        //         transform.name = name_copy + "_";
        //     }
        // }
        // transform.SetParent(null);
        

        // TODO: avoid the influence of the target. how ? 
        // just find a new name ?
        // make it belong to some specific layer ?


        // Adapt the legs just after starting the script.
        MoveLeg();
    }

    /// <summary>
    /// Method that set the preconditions for moving a leg and exectutes the coroutine process.
    /// </summary>
    public void MoveLeg()
    {
        // print("inside MoveLeg");
        // If we are already moving, don't start another move.
        if (Moving)
        {
            // print("already mooving :(");
            return;
        }

        /*
         * First, we want to calculate the distance from the GameObject where this script is attached (target, red sphere) to the home position of the respective leg (blue chip).
         * We also calculate the quaternion between the current rotation and the home rotation.
         * If such distance is larger than the threshold step, OR the angle difference is larger than the angle threshold, we call the coroutine to move the leg.
         */

        // START TODO ###################

        float distFromHome = Vector3.Distance(transform.position, homeTransform.position);
        float angleFromHome = Quaternion.Angle(transform.rotation, homeTransform.rotation);

        // Change condition!
        if (distFromHome > distanceThreshold || angleFromHome > angleThreshold)
        {
            // END TODO ###################
            // print("threshold exceeded: move baby !");

            // Get the grounded location for the feet. It can return false - in that case, it won't move.
            // This method modifies the values by reference, which are used later.
            if (GetGroundedEndPosition(out Vector3 endPos, out Vector3 endNormal))
            {
                // The foot rotation will be defined by:
                // Forward direction: Forward normalized vector of home, but projected on the same plane where the grounded position was detected.
                // Upward direction: Normal of the plane where the grounded position was detected.
                Quaternion endRot = Quaternion.LookRotation(Vector3.ProjectOnPlane(homeTransform.forward, endNormal), endNormal);

                // Start MoveFoot coroutine with the target position, rotation and duration of the movement.
                StartCoroutine(MoveFoot(endPos, endRot, moveDuration));
            }
        }
    }

    /// <summary>
    /// Method that saves the ground location information where the foot should be located.
    /// Returns false if no grounded position was found.
    /// </summary>
    /// <param name="endPos"></param>
    /// <param name="endNormal"></param>
    /// <returns></returns>
    bool GetGroundedEndPosition(out Vector3 endPos, out Vector3 endNormal)
    {
        /*
         * First, we calculate the normalized vector that goes from the current position to the home position. It will describe the direction of our overshoot (offset) vector.
         * Then, you can create a factor by taking the distance threshold and modifying it by a fraction [0, 1].
         * Finally, you can multiply such new factor by the normalized distance previously calculated.
         * The result is a vector in the direction of the movement when the foot is coming back to the home position, with a small magnitude.
         * Summing such vector to the real home position, you will get a new home position slighly moved.
         */

        Vector3 towardsHome = (homeTransform.position - transform.position).normalized;
        float overshootDistance = distanceThreshold * stepOvershootFraction;
        Vector3 overshootVector = towardsHome * overshootDistance;

        /*
         * Now, we build a raycast system. Check: https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
         * The ray will detect a collision with the terrain and use such information to place the foot accordingly on the ground.
         * First, you create the origin (Vector3) of your ray, which will be in the home position (remember to add the overshoot vector calculated before).
         * You can also add some vertical y displacement to let the ray having more space when going down and avoiding undesired collisions.
         * Then, throw the ray downwards, and save the position and normal vector of the hit in "endPos" and "endNormal" respectively.
         * If there is a collision, return true. Otherwise, you can return false.
         */

        // START TODO ###################
        float movementHeight = 2.0f;
        Vector3 raycastOrigin = homeTransform.position + overshootVector + Vector3.up * heightOffset * movementHeight;
        bool collision = Physics.Raycast(raycastOrigin, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundRaycastMask);
        if (collision)
        {
            endPos = hit.point;
            endNormal = hit.normal;
            return true;
        }

        // END TODO ###################

        endPos = Vector3.zero;
        endNormal = Vector3.zero;
        return false;
    }

    /// <summary>
    /// Coroutine that performs the motion part.
    /// </summary>
    /// <param name="endPos"></param>
    /// <param name="endRot"></param>
    /// <param name="moveTime"></param>
    /// <returns></returns>
    IEnumerator MoveFoot(Vector3 endPos, Quaternion endRot, float moveTime)
    {
        // We are in the coroutine, meaning that a moving action is taking place.
        Moving = true;

        // Store the initial, current position and rotation for the interpolation.
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        // Apply the height offset to the home position, where we will move towards.
        // You can change the heightOffset to see the effect. It just changes the vertical component to modify where the IK target is.
        endPos += homeTransform.up * heightOffset;

        // Initialize the time.
        float timeElapsed = 0;

        do
        {
            /*
             * First, you need to keep the record of the total elapsed time.
             * Then, you can normalized by using the moveTime variable.
             */

            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / moveTime;

            // We could also apply some animation curve(e.g.Easing.EaseInOutCubic) to make the foot go smoother.
            normalizedTime = Easing.EaseInOutCubic(normalizedTime);

            /*
             * We know startPos and endPos. We could interpolate directly from the starting point to the end point, but we have a problem: The movement would be straight and flat on the terrain. Try it out!
             * transform.position = Vector3.Lerp(startPoint, endPoint, normalizedTime);
             * We need to find a way to guide the foot from the ground to a lifted position, and then put it back on the ground. 
             * Any idea? Just a tip: https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Constructing_B.C3.A9zier_curves
             */

            // START TODO ###################
            //bézier curve -> interpolate the foot movement between startPos and endPos with a maximum time of moveTime
            // B(t) = (1-t)^2P0 + 2(1-t)tP1 + t^2P2 , 0 < t < 1
            Vector3 liftedPos = startPos + (endPos - startPos) * 0.5f + Vector3.up * heightOffset;
            Vector3 bezierPos = (1 - normalizedTime) * (1 - normalizedTime) * startPos + 2 * (1 - normalizedTime) * normalizedTime * liftedPos + normalizedTime * normalizedTime * endPos;
            transform.position = bezierPos;
            // END TODO ###################

            /*
             * You just need now to also move from the starting rotation and the ending rotation.
             */

            // START TODO ###################

            transform.rotation = Quaternion.Lerp(startRot, endRot, normalizedTime);

            // END TODO ###################

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveDuration);

        // Motion has finished.
        Moving = false;
    }

    /// <summary>
    /// Function for better visualization.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (Moving)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, 0.25f);
        Gizmos.DrawLine(transform.position, homeTransform.position);
        Gizmos.DrawWireCube(homeTransform.position, Vector3.one * 0.1f);
    }
}

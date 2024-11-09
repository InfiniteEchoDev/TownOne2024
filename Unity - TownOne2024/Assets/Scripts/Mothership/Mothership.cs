using UnityEngine;

public class Mothership : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    float smooth = 5.0f;
    [SerializeField]
    float tiltAngle = 60.0f;
    [SerializeField]
    Transform trans;
    [SerializeField]
    float rotateZAxis = 0.5f;
    [SerializeField]
    float turnAmount = 0.5f;

    //float tiltAroundZ = 0f;
    //float tiltAroundX = 0f;

    //Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
    Quaternion target;

    // Update is called once per frame
    void Update()
    {
        rotateZAxis = rotateZAxis+turnAmount;
        //ChangeRotationTarget();
            float tiltAroundZ = rotateZAxis * tiltAngle;
            target = Quaternion.Euler(0, 0, tiltAroundZ);
        // Dampen towards the target rotation
        
        trans.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }



}

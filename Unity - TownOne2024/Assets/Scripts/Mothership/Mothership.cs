using UnityEngine;
using System.Collections.Generic;

public class Mothership : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float rotateZAxis = 0.5f;

    [SerializeField]
    float smooth = 5.0f;

    [SerializeField]
    float tiltAngle = 20.0f;

    [SerializeField]
    float turnAmount = 0.5f;

    [SerializeField]
    public bool reverse;

    [SerializeField]
    public int sliceCount;

    //[SerializeField]
    Transform trans;

    [SerializeField]
    List<GameObject> slices = new List<GameObject>();

    List<Quaternion> fortyFiveAngles = new List<Quaternion>();
    Quaternion target;

    void Start()
    {
        trans = GetComponent<Transform>();
        SpawnSlices(sliceCount);
    }

    public bool Reverse
    {
        get { return reverse; }
        set { reverse = value; }
    }


    // Update is called once per frame
    void Update()
    {
        if (!reverse)
        {
            rotateZAxis = rotateZAxis + turnAmount;
        }
        else
        {
            rotateZAxis = rotateZAxis - turnAmount;
        }

        float tiltAroundZ = rotateZAxis * tiltAngle;
        target = Quaternion.Euler(0, 0, tiltAroundZ);

        // Dampen towards the target rotation
        trans.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }


    void SpawnSlices(int count) { 


        if (count == 4)
        {
            for(int i = 0; i < count; i++){
            Instantiate(slices[0], trans.position, Quaternion.Euler(0, 0, (90*i)), trans);
            }
        }
        else if(count == 3)
        {
            for (int i = 0; i < count; i++)
            {
                Instantiate(slices[0], trans.position, Quaternion.Euler(0, 0, (120 * i)), trans);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                Instantiate(slices[0], trans.position, Quaternion.Euler(0, 0, (180 * i)), trans);
            }
        }
    }


}

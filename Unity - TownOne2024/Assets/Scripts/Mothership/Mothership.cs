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
    int sliceCount = 24;

    [SerializeField]
    public int containerTypeCount;

    //[SerializeField]
    Transform trans;

    [SerializeField]
    List<GameObject> slices = new List<GameObject>();

    List<Quaternion> fortyFiveAngles = new List<Quaternion>();
    Quaternion target;

    void Start()
    {
        trans = GetComponent<Transform>();
        SpawnSlices(containerTypeCount);
    }

    public bool Reverse
    {
        get { return reverse; }
        set { reverse = value; }
    }

    public int SliceCount
    {
        get { return sliceCount; }
        set { sliceCount = value; }
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


    void SpawnSlices(int containerTypeCount) {


        if(containerTypeCount == 2)
        {
            for (int i = 0; i < sliceCount; i++)
            {
                if(i < sliceCount/2)
                {
                    Instantiate(slices[0], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }
                else
                {
                    Instantiate(slices[1], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }

            }
        }

        else if (containerTypeCount == 3)
        {
            for (int i = 0; i < sliceCount; i++)
            {
                if (i < sliceCount / 3)
                {
                    Instantiate(slices[0], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }
                else if(i < ((sliceCount/3)*2)){
                    Instantiate(slices[1], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }
                else
                {
                    Instantiate(slices[2], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }

            }
        }

    }


}

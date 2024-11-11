using UnityEngine;
using System.Collections.Generic;
using Gley.AllPlatformsSave.Internal;

public class Mothership : MonoBehaviour
{
    float rotateZAxis = 0.5f;

    [SerializeField]
    float smooth = 5.0f;

    float tiltAngle = 20.0f;

    [SerializeField]
    float turnAmount = 0.5f;

    [SerializeField]
    public bool reverse;

    int sliceCount = 24;

    [SerializeField]
    public int containerTypeCount;

    Transform trans;

    [SerializeField]
    List<GameObject> medSlices = new List<GameObject>();

    [SerializeField]
    List<GameObject> cargoSlices = new List<GameObject>();

    [SerializeField]
    List<GameObject> scrapSlices = new List<GameObject>();

    Quaternion target;

    void Start()
    {
        Debug.Log("fsdjifsdjf");
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
        if (GameMgr.Instance.IsGameRunning)
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
    }


    void SpawnSlices(int containerTypeCount) {

        //Half Half
        if(containerTypeCount == 2)
        {
            for (int i = 0; i < sliceCount; i++)
            {
                if(i < sliceCount/2)
                {
                    //Instantiate(scrapSlices[0], trans.position, Quaternion.Euler(0, 0, (15f * sliceCcount)), trans);
                    //Instantiate(slices[0], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }
                else
                {
                    //Instantiate(slices[1], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }

            }
        }
        //Three Even Split
        else if (containerTypeCount == 3)
        {
            for (int i = 0; i < sliceCount; i++)
            {
                
                if (i < sliceCount / 3)
                {
                    //Instantiate(slices[0], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }
                else if(i < ((sliceCount/3)*2)){
                    //Instantiate(slices[1], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }
                else
                {
                    //Instantiate(slices[2], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }

            }
        }
        //Uneven Spread
        else if (containerTypeCount > 3)
        {
            int humanRange = Random.Range(2, 4);
            int scrapRange = Random.Range(5, 6);
            int cargoRange = Random.Range(3, 4);
            int humanArrayRange = (humanRange - 1);
            int scrapArrayRange = (scrapRange - 1);
            int cargoArrayRange = (cargoRange - 1);
            int count = 0;
            while (count < sliceCount) {

                for (int i = 0; i < scrapRange; i++)
                {
                    if (count == sliceCount) break;

                    if(i == scrapArrayRange || count == 23)
                    {
                        Instantiate(scrapSlices[0], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);    
                    }
                    else if(i == 0)
                    {
                        Instantiate(scrapSlices[2], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);
                    }
                    else
                    {
                        Instantiate(scrapSlices[1], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);
                    }

                    count++;
                }
                for (int i = 0; i < humanRange; i++)
                {
                    if (count == sliceCount) break;


                    if (i == humanArrayRange || count == 23)
                    {
                        Instantiate(medSlices[0], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);
                    }
                    else if (i == 0)
                    {
                        Instantiate(medSlices[2], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);
                    }
                    else
                    {
                        Instantiate(medSlices[1], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);
                    }

                    count++;
                }
                for (int i = 0; i < cargoRange; i++)
                {
                    if (count == sliceCount) break;

                    if (i == cargoArrayRange || count == 23)
                    {
                        Instantiate(cargoSlices[0], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);
                    }
                    else if (i == 0)
                    {
                        Instantiate(cargoSlices[2], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);
                    }
                    else
                    {
                        Instantiate(cargoSlices[1], trans.position, Quaternion.Euler(0, 0, (15f * count)), trans);
                    }

                    count++;
                }

            }

            
        }


    }


}

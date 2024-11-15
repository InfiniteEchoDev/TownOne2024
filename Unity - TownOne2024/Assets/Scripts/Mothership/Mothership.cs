using UnityEngine;
using System.Collections.Generic;
using Gley.AllPlatformsSave.Internal;
using UnityEngine.Serialization;

public class Mothership : MonoBehaviour
{
    float _rotateZAxis = 0.5f;

    [FormerlySerializedAs("smooth")] [SerializeField]
    float Smooth = 5.0f;

    float _tiltAngle = 20.0f;

    [FormerlySerializedAs("turnAmount")] [SerializeField]
    float TurnAmount = 0.5f;

    [FormerlySerializedAs("reverse")] [SerializeField]
    private bool Clockwise;

    int _sliceCount = 24;

    [FormerlySerializedAs("containerTypeCount")] [SerializeField]
    public int ContainerTypeCount;

    Transform _trans;

    [FormerlySerializedAs("medSlices")] [SerializeField]
    List<GameObject> MedSlices = new List<GameObject>();

    [FormerlySerializedAs("cargoSlices")] [SerializeField]
    List<GameObject> CargoSlices = new List<GameObject>();

    [FormerlySerializedAs("scrapSlices")] [SerializeField]
    List<GameObject> ScrapSlices = new List<GameObject>();


    Quaternion _target;

    void Start()
    {
        //Debug.Log("fsdjifsdjf");
        _trans = GetComponent<Transform>();
        SpawnSlices(ContainerTypeCount);
    }

    public bool Reverse
    {
        get { return Clockwise; }
        set { Clockwise = value; }
    }

    public int SliceCount
    {
        get { return _sliceCount; }
        set { _sliceCount = value; }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            if (!Clockwise)
            {
                _rotateZAxis += TurnAmount;
            }
            else
            {
                _rotateZAxis -= TurnAmount;
            }

            float tiltAroundZ = _rotateZAxis * _tiltAngle;
            _target = Quaternion.Euler(0, 0, tiltAroundZ);

            // Dampen towards the target rotation
            _trans.rotation = Quaternion.Slerp(transform.rotation, _target, Time.deltaTime * Smooth);
        }
    }


    void SpawnSlices(int containerTypeCount) {

        //Half Half
        if(containerTypeCount == 2)
        {
            for (int i = 0; i < _sliceCount; i++)
            {
                if(i < _sliceCount/2)
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
            for (int i = 0; i < _sliceCount; i++)
            {
                
                if (i < _sliceCount / 3)
                {
                    //Instantiate(slices[0], trans.position, Quaternion.Euler(0, 0, (15f * i)), trans);
                }
                else if(i < ((_sliceCount/3)*2)){
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
            while (count < _sliceCount) {

                for (int i = 0; i < scrapRange; i++)
                {
                    if (count == _sliceCount) break;

                    if(i == scrapArrayRange || count == 23)
                    {
                        Instantiate(ScrapSlices[0], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);    
                    }
                    else if(i == 0)
                    {
                        Instantiate(ScrapSlices[2], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);
                    }
                    else
                    {
                        Instantiate(ScrapSlices[1], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);
                    }

                    count++;
                }
                for (int i = 0; i < humanRange; i++)
                {
                    if (count == _sliceCount) break;


                    if (i == humanArrayRange || count == 23)
                    {
                        Instantiate(MedSlices[0], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);
                    }
                    else if (i == 0)
                    {
                        Instantiate(MedSlices[2], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);
                    }
                    else
                    {
                        Instantiate(MedSlices[1], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);
                    }

                    count++;
                }
                for (int i = 0; i < cargoRange; i++)
                {
                    if (count == _sliceCount) break;

                    if (i == cargoArrayRange || count == 23)
                    {
                        Instantiate(CargoSlices[0], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);
                    }
                    else if (i == 0)
                    {
                        Instantiate(CargoSlices[2], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);
                    }
                    else
                    {
                        Instantiate(CargoSlices[1], _trans.position, Quaternion.Euler(0, 0, (15f * count)), _trans);
                    }

                    count++;
                }

            }

            
        }


    }


}

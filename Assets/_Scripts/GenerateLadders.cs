
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateLadders : MonoBehaviour {

    public int xLadder;
    public int zLadder;

    public GameObject ladderT;
    public GameObject ladderR;
    public GameObject ladderC;
    public GameObject ladderS;

    public Material blue;
    public Material red;

    public GameObject ladderStore;

    GameObject[] ladders = new GameObject[4];

    float ladderStep = 3.2f;
    

	// Use this for initialization
	void Start ()
    {
        ladders[0] = ladderT;
        ladders[1] = ladderR;
        ladders[2] = ladderC;
        ladders[3] = ladderS;

		for(int i = 0; i < zLadder; i++)
        {
            for(int j = 0; j < xLadder; j++)
            {
                Debug.Log("creating new ladder");
                GameObject newLadder = Instantiate(ladders[SelectLadder()], new Vector3(ladderStep * j, 0, ladderStep * i), Quaternion.Euler(SelectRotation()));
                newLadder.GetComponent<RotateLadder>().direction = SelectDirection();
                if(newLadder.GetComponent<RotateLadder>().direction == Enums.Direction.clockwise)
                {
                    newLadder.transform.GetChild(2).gameObject.GetComponentInChildren<MeshRenderer>().material = blue;
                }
                else
                {
                    newLadder.transform.GetChild(2).gameObject.GetComponentInChildren<MeshRenderer>().material = red;

                }
                newLadder.transform.parent = ladderStore.transform;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    int SelectLadder()
    {
        float select = Random.Range(0, 10);
       if(select > 8)
        {
            return 3;
        }else if(select > 6)
        {
            return 2;
        }
        else if( select > 3)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    Vector3 SelectRotation()
    {
        float select = Random.Range(0, 4);
        if(select > 3)
        {
            return new Vector3(0, 0, 0);
        }
        else if(select > 2)
        {
            return new Vector3(0, 90, 0);
        }
        else if(select > 1)
        {
            return new Vector3(0, 180, 0);
        }
        else
        {
            return new Vector3(0, 270, 0);
        }
    }

    Enums.Direction SelectDirection()
    {
        float select = Random.Range(0, 2);
        Debug.Log(select);
        if(select > 0.5)
        {
            return Enums.Direction.antiClockwise;
        }
        else
        {
            return Enums.Direction.clockwise;
        }
    }
}
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateLadder : MonoBehaviour {

    bool clickSetNewRotation;
    float degreeCount;


    public Enums.Direction direction;

    float rotationDegree;
    public float speed;

    Vector3 newDirection;
    Vector3 newRotation;

    bool canNewRotate;


    // Use this for initialization
    void Start ()
    {
        if (direction == Enums.Direction.clockwise)
        {
            rotationDegree = 90;
        }
        else
        {
            rotationDegree = -90;
        }

        speed = 1;

        newRotation = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;

        if (CheckEqualRotation(transform.rotation.eulerAngles, newRotation))
        {
            canNewRotate = true;
        }
        else
        {
            canNewRotate = false;
            clickSetNewRotation = false;
        }

        if (clickSetNewRotation && canNewRotate)
        {
            canNewRotate = false;
            Debug.Log("setting new value");
            newRotation = new Vector3(0, this.transform.rotation.eulerAngles.y + rotationDegree, 0);

            clickSetNewRotation = false;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(newRotation), speed);

    }

    /*
    * On mouse click on the collider of the object - 
    * check that gameObject is a Ladder - 
    * Try and set new rotation
    */
    private void OnMouseDown()
    {
        Debug.Log("Click");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Ladder")
            {
                clickSetNewRotation = true;
            }
            else
            {
                Debug.Log("This isn't a Player");
            }
        }
    }

    private Vector3 NormalizeRotationEulur(Vector3 rotation)
    {
        Vector3 start = rotation;
        if(rotation.y == -90f)
        {
            rotation.y = 270f;
        }
        
        if(rotation.y == 360f)
        {
            Debug.Log("reset");
            rotation.y = 0.0f;
        }

        Debug.Log(rotation + "  " + start);

        return Vector3.Normalize(rotation);
    }

    bool CheckEqualRotation(Vector3 currentRotation, Vector3 targetRotation)
    {
        if(currentRotation.y == targetRotation.y)
        {
            return true;
        }
        if(currentRotation.y == 360 && (int)targetRotation.y == 0)
        {
            return true;
        }
        if((int)currentRotation.y == 0 && targetRotation.y == 360)
        {
            return true;
        }
        if(currentRotation.y == -90 && targetRotation.y == 270)
        {
            return true;
        }
        if(currentRotation.y == 270 && targetRotation.y == -90)
        {
            return true;
        }

        return false;
    }
}

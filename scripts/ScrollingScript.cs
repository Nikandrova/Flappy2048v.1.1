using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour { 

    //бесконечный фон
    public bool isLooping = true;
    public Transform barrierUp;
    public Transform barrierDown;

    float positionY;

    void Start()
    {
        positionY = 135f;
    }

    void Update()
    {
        if (isLooping)
        {
            if (barrierUp != null)
            {
                //находится ли объект перед камерой?
                if ((barrierUp.GetComponent<Transform>().position.x < Camera.main.transform.position.x))                    
                {
                    if(barrierUp.GetComponent<Transform>().position.x < -340f)
                    {
                        positionY = Random.Range(170f, 60f);
                        barrierUp.GetComponent<Transform>().position = new Vector3(330, positionY, 230);
                        barrierDown.GetComponent<Transform>().position = new Vector3(330, positionY - 235, 230);
                    }
                }

            }
        }
    }
}


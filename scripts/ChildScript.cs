using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour {

    public GameLogic logic;
    public Transform catchCubeWillChild;

    void Start()
    {
        logic = FindObjectOfType<GameLogic>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag != "catch") && (other.tag != "Player"))
        {
            for (int i = 0; i < logic.playerChild.Count; i++)
            {
                var cube = logic.playerChild[i];
                if (cube.GetComponent<PlayerScript>() != null)
                {
                    cube.GetComponent<PlayerScript>().direction = new Vector2();
                    cube.GetComponent<PlayerScript>().speed = new Vector2();
                }
                if (cube.GetComponent<SpringJoint2D>() != null)
                    cube.GetComponent<SpringJoint2D>().enabled = false;
                cube.GetComponent<BoxCollider2D>().enabled = true;
                cube.GetComponent<BoxCollider2D>().isTrigger = false;
                cube.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                if (i != 0)
                    cube.GetComponent<Rigidbody2D>().gravityScale = 5;
            }
            var barrierUp = GameObject.Find("barrierUpper");
            if (barrierUp != null)
            {
                barrierUp.GetComponent<MoveScript>().enabled = false;
                barrierUp.GetComponent<ScrollingScript>().enabled = false;
                barrierUp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            var barrierLow = GameObject.Find("barrierLower");
            if (barrierLow != null)
            {
                barrierLow.GetComponent<MoveScript>().enabled = false;
                barrierLow.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            catchCubeWillChild.GetComponent<MoveScript>().enabled = false;
            catchCubeWillChild.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.parent.gameObject.AddComponent<GameOverScript>();
        }      
    }
}

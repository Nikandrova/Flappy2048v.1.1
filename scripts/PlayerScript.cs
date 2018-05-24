using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Vector2 direction = new Vector2(0, 1);
    public Vector2 speed = new Vector2(1, 20);
    private Vector2 movement;

    public Transform catchCubeWillChild;
    public Transform player;

    public GameObject barrierFlow;

    private Rigidbody2D rb;

    public GameLogic logic;

    void Start()
    {
        logic = FindObjectOfType<GameLogic>();
        rb = GetComponent<Rigidbody2D>();

        if (logic.playerChild.Count == 0)
            logic.playerChild.Add(player);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            movement = new Vector2(0, direction.y * speed.y);
            rb.AddForce(movement, ForceMode2D.Impulse);
        }
    }

    public void createChildCube(Transform transform)
    {
        Transform catchChild = Instantiate(transform);
        catchChild.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        catchChild.tag = "childPlayer";
        logic.playerChild.Add(catchChild);
    }

    public void cubesEqual(List<int> number)
    {
        for (int i = 0; i < logic.playerChild.Count - 1; i++)
        {
            if (number[i] == number[i + 1])
            {
                var obj = logic.playerChild[i + 1];
                logic.playerChild.RemoveAt(i + 1);
                Destroy(obj.gameObject);
                number.RemoveAt(i + 1);
                number[i] *= 2;
                logic.playerChild[i].GetComponentInChildren<Text>().text = number[i].ToString();
            }
        }
    }

    public void cubeProperties(List<int> number)
    {
        for (int i = 0, j = logic.playerChild.Count; i < logic.playerChild.Count; i++, j--)
        {
            var pObject = logic.playerChild[i];
            if ((pObject != null) && (pObject != logic.playerChild[0]))
            {
                var plChild = logic.playerChild[i];
                plChild.GetComponent<Transform>().position =
                    new Vector3(logic.playerChild[i - 1].GetComponent<RectTransform>().anchoredPosition.x - 37f,
                    logic.playerChild[i - 1].GetComponent<RectTransform>().anchoredPosition.y,
                    230);

                plChild.GetComponent<SpringJoint2D>().enabled = true;
                plChild.GetComponent<SpringJoint2D>().distance = 3f;
                plChild.GetComponent<SpringJoint2D>().frequency = 7f;
                plChild.GetComponent<SpringJoint2D>().anchor =
                    new Vector2(plChild.GetComponent<RectTransform>().anchoredPosition.x - 37f,
                    plChild.GetComponent<RectTransform>().anchoredPosition.y);
                plChild.GetComponent<SpringJoint2D>().connectedAnchor =
                    new Vector2(plChild.GetComponent<RectTransform>().anchoredPosition.x,
                    plChild.GetComponent<RectTransform>().anchoredPosition.y);
                plChild.GetComponent<SpringJoint2D>().connectedBody = logic.playerChild[i - 1].GetComponent<Rigidbody2D>();

                plChild.GetComponent<catchScript>().enabled = false;
                plChild.GetComponent<MoveScript>().enabled = false;

                plChild.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                plChild.GetComponent<Rigidbody2D>().constraints =
                    RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                plChild.GetComponent<Rigidbody2D>().mass = 0.1f;
                plChild.GetComponent<Rigidbody2D>().gravityScale = 1;

                plChild.GetComponent<BoxCollider2D>().isTrigger = true;
                if (plChild.GetComponent<ChildScript>() == null)
                {
                    plChild.gameObject.AddComponent<ChildScript>();
                    plChild.GetComponent<ChildScript>().logic = FindObjectOfType<GameLogic>();
                    plChild.GetComponent<ChildScript>().catchCubeWillChild = catchCubeWillChild;
                }
            }
        }
    }

    public void arrangementNumber(List<int> number)
    {
        if (logic.playerChild.Count == 2)
        {
            if (number[number.Count - 1] == 2)
                for (int i = 0, j = logic.playerChild.Count - 1; i < logic.playerChild.Count; i++, j--)
                    logic.playerChild[i].GetComponentInChildren<Text>().text = (number[j]).ToString();
        }
        else
        {
            for (int i = 0; i < logic.playerChild.Count; i++)
            {
                if ((number[number.Count - 1] < 16))// && (number.Count < 3))
                    if (i == 0)
                        logic.playerChild[0].GetComponentInChildren<Text>().text = (number[number.Count - 1]).ToString();
                    else
                        logic.playerChild[i].GetComponentInChildren<Text>().text = (number[i - 1]).ToString();

            }
        }
    }

    public void addingNumberCubes(List<int> number)
    {
        for (int i = 0; i < logic.playerChild.Count; i++)
            number.Add(int.Parse(logic.playerChild[i].GetComponentInChildren<Text>().text));
    }

    public void sizeFont(List<int> number)
    {
        for (int i = 0; i < logic.playerChild.Count; i++)
            if (int.Parse(logic.playerChild[i].GetComponentInChildren<Text>().text) >= 128)
            {
                logic.playerChild[i].GetComponentInChildren<Text>().fontSize = 20;
                logic.playerChild[i].GetComponentInChildren<Text>().rectTransform.anchoredPosition
                         = new Vector2(0f, -5.5f);
            }
            else
            {
                logic.playerChild[i].GetComponentInChildren<Text>().fontSize = 30;
                logic.playerChild[i].GetComponentInChildren<Text>().rectTransform.anchoredPosition
                        = new Vector2(0f, -5.5f);
            }
    }

    public void breakeJoin()
    {
        for(int i = 0; i < logic.playerChild.Count; i++)
        {
            var cube = logic.playerChild[i];
            if(cube.GetComponent<PlayerScript>() != null)
            {
                direction = new Vector2();
                speed = new Vector2();
            }
            if(cube.GetComponent<SpringJoint2D>() != null)
                cube.GetComponent<SpringJoint2D>().enabled = false;
            cube.GetComponent<BoxCollider2D>().enabled = true;
            cube.GetComponent<BoxCollider2D>().isTrigger = false;
            cube.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            if (i != 0)
                cube.GetComponent<Rigidbody2D>().gravityScale = 5;
        }
        var barrierUp = GameObject.Find("barrierUpper");
        if(barrierUp != null)
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
    }

    public void addCube(List<int> number)
    {
        addingNumberCubes(number);
        int plChildCount = logic.playerChild.Count;
        for (int j = 0; j <= plChildCount; j++)
            cubesEqual(number);
        cubeProperties(number);
        arrangementNumber(number);
        sizeFont(number);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "catch")
        {
            List<int> number = new List<int>();
            if (catchCubeWillChild.GetComponentInChildren<Text>().text == logic.playerChild[0].GetComponentInChildren<Text>().text)
            {
                logic.playerChild[0].GetComponentInChildren<Text>().text =
                    (int.Parse(logic.playerChild[0].GetComponentInChildren<Text>().text) * 2).ToString();
                addCube(number);
            }
            else
            {
                createChildCube(catchCubeWillChild);
                addCube(number);
            }
        }
        else
        {
            breakeJoin();
            transform.parent.gameObject.AddComponent<GameOverScript>();
        }
    }
}
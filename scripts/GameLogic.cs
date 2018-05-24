using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public List<Transform> playerChild;

    void Awake()
    {
        playerChild = new List<Transform>();
    }
}

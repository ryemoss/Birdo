using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csLevelNode : MonoBehaviour
{
    public int levelindex;

    void OnMouseDown()
    {
        GameObject.Find("MenuLevelSelect").GetComponent<csLevelSelect>().NodeClicked(gameObject.GetComponent<CircleCollider2D>());
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csDontDestroyOnLoad: MonoBehaviour
{
	void Start ()
	{
        DontDestroyOnLoad(gameObject);
	}
}
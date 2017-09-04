using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csDeathParticles : MonoBehaviour
{
	void Start() 
	{
        Destroy(gameObject, 2f);
	}
}
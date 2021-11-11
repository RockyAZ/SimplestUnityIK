using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegs : MonoBehaviour
{
	public Transform goalPosition;

	[SerializeField] private Transform[] limbs;
	private float[] lens;//lengths between bones(limbs)

	void SaveLimbsLen()
	{
		for (int i = 0; i < limbs.Length - 1; i++)
			lens[i] = Vector3.Magnitude(limbs[i].position - limbs[i + 1].position);
	}

	void Awake()
	{
		//limbs = GetComponentsInParent<Transform>();//save all parent GameObject.Transform to array
		//limbs = GetComponentsInChildren<Transform>();
		foreach (Transform limb in limbs)
			print(limb.name);

		lens = new float[limbs.Length - 1];
		SaveLimbsLen();//save lengths between bones(limbs)
	}

	void Update()
	{
		FABRIK();
	}

	void FABRIK()
	{
		FinalToRoot();//first phase 
		RootToFinal();//second phase
	}

	void FinalToRoot()
	{
		limbs[0].position = goalPosition.position;
		for (int i = 1; i < limbs.Length - 1; i++)
			limbs[i].position = limbs[i - 1].position + ((limbs[i].position - limbs[i - 1].position).normalized * lens[i]);
	}

	void RootToFinal()
	{
		for (int i = limbs.Length - 2; i >= 0; i--)
		{
			limbs[i].position = limbs[i + 1].position + ((limbs[i].position - limbs[i + 1].position).normalized * lens[i]);
			limbs[i].LookAt(limbs[i + 1], Vector3.forward);
			limbs[i].Rotate(Vector3.up, 90);
		}
	}
}


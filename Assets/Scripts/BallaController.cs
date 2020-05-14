using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallaController : MonoBehaviour {

	public int segmentCount;
	public float segmentScale;
	[SerializeField]
	private float fireStrength;
	[SerializeField]
	private GameObject ballPathHolder;
	[SerializeField]
	private bool checkEnd = false;
	[SerializeField]
	private bool drawPath = false;
	[SerializeField]
	private Vector3[] segments;
	private void Start()
	{
		segments = new Vector3[segmentCount];
		ballPathHolder = GameObject.Find("BallPathHolder");
		ballPathHolder.transform.position = transform.position;
	}

	private void FixedUpdate()
	{
		if(drawPath)
		{
			segments[0] = ballPathHolder.transform.position;
			Vector3 segVelocity = new Vector3(fireStrength, fireStrength, 0);

			for (int i = 1; i < segmentCount; i++)
			{
				float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;
				segVelocity = segVelocity + Physics.gravity * segTime;
				segments[i] = segments[i - 1] + segVelocity * segTime;
			}

			ClearPath();

			for (int i = 0; i < segmentCount; i++)
			{
				Instantiate(Resources.Load<GameObject>("Prefabs/UI/Dot"), segments[i], new Quaternion(), ballPathHolder.transform);
			}
		}
		else
		{
			ClearPath();
		}
	}

	internal void SetFireStrength(float power)
	{
		drawPath = true;
		fireStrength = power;
	}

	public void ClearPath()
	{
		if (ballPathHolder.transform.childCount >= segmentCount)
			for (int i = 0; i < ballPathHolder.transform.childCount; i++)
			{
				Destroy(ballPathHolder.transform.GetChild(i).gameObject);
			}
	}

	internal void StartCheckEnd()
	{
		StartCoroutine(CheckEnd());
	}

	IEnumerator CheckEnd()
	{
		drawPath = false;
		yield return new WaitForSeconds(0.1f);
		checkEnd = true;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Finish"))
		{
			Destroy(gameObject);
			GameObject.Find("MusicController(Clone)").transform.Find("Fx_Hole").GetComponent<AudioSource>().Play();
			Instantiate(Resources.Load<GameObject>("Prefabs/UI/GoalText"), GameObject.Find("Canvas").transform, false);
			GameObject.Find("GameplayController").GetComponent<GameplayController>().CreateFlag();
		}
	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if(checkEnd)
		{
			Destroy(gameObject);
			GameObject.Find("MusicController(Clone)").transform.Find("Fx_Ground").GetComponent<AudioSource>().Play();
			GameObject.Find("GameplayController").GetComponent<GameplayController>().End();
		}
	}
}

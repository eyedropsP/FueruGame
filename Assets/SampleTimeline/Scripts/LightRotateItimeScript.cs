using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class LightRotateItimeScript : MonoBehaviour, ITimeControl
{
	public GameObject Dlight;


	
	//クリップ再生中
	public void SetTime( double time )
	{
		
	}

	//クリップの開始時
	public void OnControlTimeStart()
	{
		Dlight.transform.rotation = Quaternion.Euler( -90f, 0f, 0f );
	}

	//クリップの終了時
	public void OnControlTimeStop()
	{
		Dlight.transform.rotation = Quaternion.Euler( 50f, 0f, 0f );
	}
}

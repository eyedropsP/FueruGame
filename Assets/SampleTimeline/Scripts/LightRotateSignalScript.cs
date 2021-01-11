using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotateSignalScript : MonoBehaviour
{
	GameObject Dlight;

	private void Awake()
	{
		Dlight = GameObject.Find( "Directional Light" );
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void LightRotation_method()
	{
		Dlight.transform.rotation = Quaternion.Euler( -90f, 0f, 0f );

	}
	public void LightRotationBack_method()
	{
		Dlight.transform.rotation = Quaternion.Euler( 50f, 0f, 0f );

	}
}

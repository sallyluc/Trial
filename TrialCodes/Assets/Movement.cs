using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class Movement : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public float sphereRadius;

	public GameObject trig;

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax)
			);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
		CheckOverlap();
	}
		
	void CheckOverlap()
	{
		if (Physics.CheckSphere (GetComponent<Collider>().bounds.center,GetComponent<Collider>().bounds.size.x)) {
			Collider[] hits = Physics.OverlapSphere(GetComponent<Collider>().bounds.center,GetComponent<Collider>().bounds.size.x);

			for (int i = 0; i < hits.Length; i++) {
				if (hits [i].gameObject.name == "Sphere") {
					Debug.Log ("Sphere found");
					break;
				}
			}

		}
	}
}
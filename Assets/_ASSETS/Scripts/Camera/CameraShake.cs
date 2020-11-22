using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	private Transform camTransform;

	// How long the object should shake for.
	public float ShakeDuration
    {
		get => shakeDuration;
		set => shakeDuration = value;
    }
	[SerializeField] private float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float ShakeAmount
    {
		get => shakeAmount;
		set => shakeAmount = value;
    }
	[SerializeField] private float shakeAmount = 0.7f;
	public float DecreaseFactor
    {
		get => decreaseFactor;
		set => decreaseFactor = value;
    }
	[SerializeField] private float decreaseFactor = 1.0f;

	private Vector3 resetPos;
	private CameraMove cameraMove;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
		if (cameraMove == null)
		{
			cameraMove = GetComponent(typeof(CameraMove)) as CameraMove;
		}
	}

	void Update()
	{
		resetPos = cameraMove.Target.transform.position;
		resetPos.z = cameraMove.transform.position.z;
		if (shakeDuration > 0)
		{
			var newPos = resetPos + Random.insideUnitSphere * shakeAmount;
			newPos.z = resetPos.z;
			camTransform.localPosition = newPos;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			//camTransform.localPosition = resetPos;
		}
	}

	public void SetShakeDuration(float duration)
    {
		shakeDuration = duration;
    }
}
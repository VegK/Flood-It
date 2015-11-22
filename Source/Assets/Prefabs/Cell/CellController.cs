using UnityEngine;
using System.Collections;

public class CellController : MonoBehaviour
{
	#region Properties
	#region Public
	public GameObject FirstSide;
	public GameObject SecondSide;

	public float SpeedRotation = 120f;
	#endregion
	#region Private
	private Material _firstMaterial;
	private Material _secondMaterial;

	private bool _rotate = false;
	private bool _topSide = true;
	private float _time;
	private Vector3 _rotate0;
	private Vector3 _rotate180;
	private Vector3 _rotate360;
	#endregion
	#endregion

	#region Methods
	#region Public
	public void SetColor(Color color)
	{
		_time = Time.time;
		if (_topSide)
		{
			_secondMaterial.color = color;
			_topSide = false;
		}
		else
		{
			_firstMaterial.color = color;
			_topSide = true;
		}
		_rotate = true;
	}
	#endregion
	#region Private
	private void Awake()
	{
		_firstMaterial = FirstSide.GetComponent<MeshRenderer>().material;
		_secondMaterial = SecondSide.GetComponent<MeshRenderer>().material;

		_rotate0 = new Vector3(0, 0, 315);
		_rotate180 = new Vector3(180, 0, 315);
		_rotate360 = new Vector3(360, 0, 315);
	}

	private void OnMouseDown()
	{
		var arrColor = new Color[] { Color.red, Color.green, Color.cyan, Color.yellow, Color.black, Color.gray, Color.blue, Color.magenta };
		SetColor(arrColor[Random.Range(0, 8)]);
	}

	private void Update()
	{
		if (!_rotate)
			return;

		var time = (Time.time - _time) * Time.deltaTime * SpeedRotation;
		var start = Quaternion.Euler(_topSide ? _rotate180 : _rotate0);
		var end = Quaternion.Euler(_topSide ? _rotate360 : _rotate180);
		transform.localRotation = Quaternion.Lerp(start, end, time);
	}
	#endregion
	#endregion
}
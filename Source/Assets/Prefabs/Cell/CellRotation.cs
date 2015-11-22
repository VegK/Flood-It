using UnityEngine;
using System.Collections;

namespace Cell
{
	public class CellRotation : MonoBehaviour
	{
		#region Properties
		#region Public

		#endregion
		#region Private
		private CellController _cellController;

		private float _speed;
		private bool _rotate = false;
		private float _time;
		private Vector3 _rotate0;
		private Vector3 _rotate180;
		private Vector3 _rotate360;
		#endregion
		#endregion

		#region Methods
		#region Public
		public void Rotate(float speed)
		{
			_time = Time.time;
			_speed = speed;
			_rotate = true;
		}
		#endregion
		#region Private
		private void Awake()
		{
			_cellController = GetComponentInParent<CellController>();

			_rotate0 = new Vector3(0, 0, 315);
			_rotate180 = new Vector3(180, 0, 315);
			_rotate360 = new Vector3(360, 0, 315);
		}

		private void Update()
		{
			if (!_rotate)
				return;

			var time = (_speed < 0) ? 1 : (Time.time - _time) * Time.deltaTime * _speed;
			var start = Quaternion.Euler(_cellController.TopSide ? _rotate180 : _rotate0);
			var end = Quaternion.Euler(_cellController.TopSide ? _rotate360 : _rotate180);
			transform.localRotation = Quaternion.Lerp(start, end, time);

			if (time >= 1)
				_rotate = false;
		}
		#endregion
		#endregion
	}
}
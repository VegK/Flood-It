using UnityEngine;
using System.Collections;

namespace Cell
{
	public class CellController : MonoBehaviour
	{
		#region Properties
		#region Public
		public GameObject BaseCell;
		public GameObject FirstSide;
		public GameObject SecondSide;

		public float SpeedRotation = 120f;
		
		[HideInInspector]
		public bool TopSide = true;
		#endregion
		#region Private
		private CellRotation _rotation;
		private CellColor _color;
		#endregion
		#endregion

		#region Methods
		#region Public

		#endregion
		#region Private
		private void Awake()
		{
			_rotation = BaseCell.GetComponent<CellRotation>();
			_color = BaseCell.GetComponent<CellColor>();
		}

		private void OnMouseDown()
		{
			var arrColor = new Color[] { Color.red, Color.green, Color.cyan, Color.yellow, Color.black, Color.gray, Color.blue, Color.magenta };
			_color.SetColor(arrColor[Random.Range(0, 8)]);

			_rotation.Rotate(SpeedRotation);

			TopSide = !TopSide;
		}
		#endregion
		#endregion
	}
}
using UnityEngine;

namespace Cell
{
	public class CellController : MonoBehaviour
	{
		public event System.EventHandler MouseDown;

		#region Properties
		#region Public
		public GameObject BaseCell;
		public GameObject FirstSide;
		public GameObject SecondSide;

		public float SpeedRotation = 120f;
		
		public bool TopSide { get; private set; }
		public Color Color { get; private set; }
		public int X { get; set; }
		public int Y { get; set; }
		#endregion
		#region Private
		private CellRotation _rotation;
		private CellColor _color;
		#endregion
		#endregion

		#region Methods
		#region Public
		public void SetColor(Color color)
		{
			Color = color;
			_color.SetColor(color);
			_rotation.Rotate(SpeedRotation);
			TopSide = !TopSide;
		}
		#endregion
		#region Private
		private void Awake()
		{
			TopSide = true;
			_rotation = BaseCell.GetComponent<CellRotation>();
			_color = BaseCell.GetComponent<CellColor>();
		}

		private void OnMouseDown()
		{
			if (MouseDown != null)
				MouseDown(this, null);
		}
		#endregion
		#endregion
	}
}
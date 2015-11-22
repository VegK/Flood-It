using UnityEngine;
using System.Collections;

namespace Cell
{
	public class CellColor : MonoBehaviour
	{
		#region Properties
		#region Public

		#endregion
		#region Private
		private CellController _cellController;
		private Material _firstMaterial;
		private Material _secondMaterial;
		#endregion
		#endregion

		#region Methods
		#region Public
		public void SetColor(Color color)
		{
			if (_cellController.TopSide)
				_secondMaterial.color = color;
			else
				_firstMaterial.color = color;
		}
		#endregion
		#region Private
		private void Awake()
		{
			_cellController = GetComponentInParent<CellController>();

			_firstMaterial = _cellController.FirstSide.GetComponent<MeshRenderer>().material;
			_secondMaterial = _cellController.SecondSide.GetComponent<MeshRenderer>().material;
		}
		#endregion
		#endregion
	}
}
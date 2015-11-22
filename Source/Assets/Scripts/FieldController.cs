using UnityEngine;
using System.Collections;

public class FieldController : MonoBehaviour
{
	#region Properties
	#region Public
	[Header("Prefabs")]
	public Cell.CellController PrefabCell;
	[Header("Config")]
	public int Width = 10;
	public int Height = 10;
	#endregion
	#region Private
	private Cell.CellController[,] _cells;
	#endregion
	#endregion

	#region Methods
	#region Public

	#endregion
	#region Private
	private void Start()
	{
		_cells = new Cell.CellController[Width, Height];
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				var obj = Instantiate(PrefabCell);
				obj.transform.SetParent(transform, false);
				obj.name = "Cell " + x + ":" + y;
				obj.transform.position = new Vector3(x, y, transform.position.z);
				_cells[x, y] = obj;
			}
		}
	}
	
	private void Update()
	{
	
	}
	#endregion
	#endregion
}
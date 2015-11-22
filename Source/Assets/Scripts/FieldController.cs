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
		var indentX = (float)Width / 2 - 0.5f;
		var indentY = (float)Height / 2 - 0.5f;

		_cells = new Cell.CellController[Width, Height];
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				var obj = Instantiate(PrefabCell);
				obj.transform.SetParent(transform, false);
				obj.name = "Cell " + x + ":" + y;

				var pos = transform.position;
				pos.x += x - indentX;
				pos.y += y - indentY;
				obj.transform.position = pos;

				_cells[x, y] = obj;
			}
		}
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		var indentX = (float)Width / 2 - 0.5f;
		var indentY = (float)Height / 2 - 0.5f;
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				var pos = transform.position;
				pos.x += x - indentX;
				pos.y += y - indentY;
				Gizmos.DrawWireCube(pos, Vector2.one);
			}
		}
	}
	#endregion
	#endregion
}
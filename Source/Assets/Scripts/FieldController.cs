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

		StartCoroutine(PaintCells());
	}

	private IEnumerator PaintCells()
	{
		while (Time.time < 1)
			yield return null;

		var arrColor = new Color[] { Color.red, Color.green, Color.blue };

		for (int x = 0; x <= Width; x++)
		{
			for (int z = 0; z < x; z++)
			{
				var y = Height - x + z;
				if (y >= 0)
					_cells[z, y].SetColor(arrColor[Random.Range(0, arrColor.Length)]);
			}
			yield return new WaitForSeconds(0.1f);
		}

		for (int y = Height - 1; y >= 0; y--)
		{
			for (int z = 0; z < y; z++)
			{
				var x = Width - y + z;
				if (x >= 0)
					_cells[x, z].SetColor(arrColor[Random.Range(0, arrColor.Length)]);
			}
			yield return new WaitForSeconds(0.1f);
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
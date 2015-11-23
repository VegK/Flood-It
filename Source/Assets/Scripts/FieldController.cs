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
	private bool _lockClick = true;
	#endregion
	#endregion

	#region Methods
	#region Public
	public void LinkedRestartLevel()
	{
		MainInterfaceController.Instance.RestartEvent += new System.EventHandler((s, e) =>
		{
			ClearField();
			CreateField();
		});
	}
	#endregion
	#region Private
	private void Start()
	{
		CreateField();
	}

	private void ClearField()
	{
		StopAllCoroutines();

		var childsCount = transform.childCount;
		for (int i = 0; i < childsCount; i++)
			Destroy(transform.GetChild(i).gameObject);
	}

	private void CreateField()
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

				obj.MouseDown += CellMouseDown;
				obj.X = x;
				obj.Y = y;
				_cells[x, y] = obj;
			}
		}

		StartCoroutine(PaintCellsRandomColors());
	}

	private IEnumerator PaintCellsRandomColors()
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
				{
					_cells[z, y].Color = arrColor[Random.Range(0, arrColor.Length)];
					_cells[z, y].Flip(_cells[z, y].Color);
				}
			}
			yield return new WaitForSeconds(0.1f);
		}

		for (int y = Height - 1; y >= 0; y--)
		{
			for (int z = 0; z < y; z++)
			{
				var x = Width - y + z;
				if (x >= 0)
				{
					_cells[x, z].Color = arrColor[Random.Range(0, arrColor.Length)];
					_cells[x, z].Flip(_cells[x, z].Color);
				}
			}
			yield return new WaitForSeconds(0.1f);
		}

		_lockClick = false;
	}

	private IEnumerator PaintCell(Color color)
	{
		var baseColor = _cells[0, Height - 1].Color;
		if (color == baseColor)
			yield break;

		var arrPaint = GetPaintArray(baseColor, color);

		for (int x = 0; x <= Width; x++)
		{
			for (int z = 0; z < x; z++)
			{
				var y = Height - x + z;
				if (y >= 0 && arrPaint[z, y] != null)
					arrPaint[z, y].Flip(color);
			}
			yield return new WaitForSeconds(0.1f);
		}

		for (int y = Height - 1; y >= 0; y--)
		{
			for (int z = 0; z < y; z++)
			{
				var x = Width - y + z;
				if (x >= 0 && arrPaint[x, z] != null)
					arrPaint[x, z].Flip(color);
			}
			yield return new WaitForSeconds(0.1f);
		}

		GameOver();
	}

	private Cell.CellController[,] GetPaintArray(Color baseColor, Color newColor)
	{
		var arrPaint = new Cell.CellController[Width, Height];
		var cells = new ArrayList();

		cells.Add(_cells[0, Height - 1]);
		while (cells.Count > 0)
		{
			var cell = cells[0] as Cell.CellController;

			for (int x = cell.X; x >= 0; x--)
			{
				if (_cells[x, cell.Y].Color != baseColor)
					break;

				arrPaint[x, cell.Y] = _cells[x, cell.Y];

				if (cell.Y + 1 < Height)
					if (arrPaint[x, cell.Y + 1] == null &&
						_cells[x, cell.Y + 1].Color == baseColor)
						cells.Add(_cells[x, cell.Y + 1]);

				if (cell.Y - 1 >= 0)
					if (arrPaint[x, cell.Y - 1] == null &&
						_cells[x, cell.Y - 1].Color == baseColor)
						cells.Add(_cells[x, cell.Y - 1]);
			}

			for (int x = cell.X; x < Width; x++)
			{
				if (_cells[x, cell.Y].Color != baseColor)
					break;

				arrPaint[x, cell.Y] = _cells[x, cell.Y];

				if (cell.Y + 1 < Height)
					if (arrPaint[x, cell.Y + 1] == null &&
						_cells[x, cell.Y + 1].Color == baseColor)
						cells.Add(_cells[x, cell.Y + 1]);

				if (cell.Y - 1 >= 0)
					if (arrPaint[x, cell.Y - 1] == null &&
						_cells[x, cell.Y - 1].Color == baseColor)
						cells.Add(_cells[x, cell.Y - 1]);
			}

			cells.Remove(cell);
		}

		for (int x = 0; x < Width; x++)
			for (int y = 0; y < Height; y++)
				if (arrPaint[x, y] != null)
					arrPaint[x, y].Color = newColor;

		return arrPaint;
	}

	private void CellMouseDown(object sender, System.EventArgs args)
	{
		if (_lockClick)
			return;

		var cell = sender as Cell.CellController;
		if (cell == null)
			return;

		StartCoroutine(PaintCell(cell.Color));
	}

	private void GameOver()
	{
		var countColor = 0;
		var baseColor = _cells[0, Height - 1].Color;

		for (int x = 0; x < Width; x++)
			for (int y = 0; y < Height; y++)
				if (_cells[x, y].Color == baseColor)
					countColor++;

		if (countColor < Width * Height)
			return;

		MainInterfaceController.Instance.Show();
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
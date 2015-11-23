using System;
using UnityEngine;
using UnityEngine.UI;

public class MainInterfaceController : MonoBehaviour
{
	#region Properties
	#region Public
	public Text UISteps;
	public Text UITime;

	public static MainInterfaceController Instance;

	public event EventHandler RestartEvent;

	public int Steps
	{
		get
		{
			return _steps;
		}
		set
		{
			_steps = value;
			UISteps.text = "Steps: " + _steps;
		}
	}
	public int Time
	{
		get
		{
			return _time;
		}
		set
		{
			_time = value;
			UITime.text = "Time: " + _time + " sec";
		}
	}
	#endregion
	#region Private
	private int _steps;
	private int _time;
	#endregion
	#endregion

	#region Methods
	#region Public
	public void Show(int steps, int time)
	{
		Steps = steps;
		Time = time;
		gameObject.SetActive(true);
	}

	public void OnClickRestart()
	{
		if (RestartEvent == null)
			return;
		RestartEvent(this, null);

		gameObject.SetActive(false);
	}
	#endregion
	#region Private
	private void Awake()
	{
		Instance = this;
		gameObject.SetActive(false);
	}
	#endregion
	#endregion
}
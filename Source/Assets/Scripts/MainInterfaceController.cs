using System;
using UnityEngine;
using UnityEngine.UI;

public class MainInterfaceController : MonoBehaviour
{
	public event EventHandler RestartEvent;

	#region Properties
	#region Public
	[Header("Game interface")]
	public GameObject UIPanelGI;
	public Text UIStepsGI;
	public Text UITimeGI;
	[Header("Game over")]
	public GameObject UIPanelGO;
	public Text UIStepsGO;
	public Text UITimeGO;

	public static MainInterfaceController Instance;

	public int Steps
	{
		set
		{
			UIStepsGI.text = value.ToString();
		}
	}
	public int Time
	{
		set
		{
			var ts = new TimeSpan(0, 0, value);
			var min = ts.Minutes.ToString("D2");
			var sec = ts.Seconds.ToString("D2");
			UITimeGI.text = min + ":" + sec;
		}
	}
	#endregion
	#region Private
	
	#endregion
	#endregion

	#region Methods
	#region Public
	public void ShowGameOver(int steps, int time)
	{
		UIPanelGI.gameObject.SetActive(false);

		UIStepsGO.text = "Steps: " + steps;
		UITimeGO.text = "Time: " + time + " sec";
		UIPanelGO.gameObject.SetActive(true);
	}

	public void OnClickRestart()
	{
		if (RestartEvent == null)
			return;
		RestartEvent(this, null);

		Steps = 0;
		Time = 0;
		UIPanelGI.gameObject.SetActive(true);

		UIPanelGO.gameObject.SetActive(false);
	}
	#endregion
	#region Private
	private void Awake()
	{
		Instance = this;

		Steps = 0;
		Time = 0;

		UIPanelGO.gameObject.SetActive(false);
	}
	#endregion
	#endregion
}
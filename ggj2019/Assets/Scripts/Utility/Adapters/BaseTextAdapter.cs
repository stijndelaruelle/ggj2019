using System.Linq;
using UnityEngine;

/// <summary>
/// Automatically updates a text field based on assigned Base Values 
/// </summary>
public abstract class BaseTextAdapter : BaseUIAdapter
{
	public BaseValue[] values = null;

	protected override BaseValue Value
	{
		get { return values.Length > 0 ? values[0] : null; }
	}

	public void UpdateText()
	{
		Debug.Assert(values != null, "Values can not be null", this.gameObject);

		SetText(values.Select(x => x.ToObject()).ToArray());
	}

	private void SetText(params object[] parameters)
	{
		ApplyText(parameters[0].ToString());
	}

	protected abstract void ApplyText(string text);

	protected override void OnValueChanged(BaseValue value)
	{
		if (this == null)
		{
			Debug.Log("Adapter has been destroyed");
			return;
		}

		UpdateText();
	}
}

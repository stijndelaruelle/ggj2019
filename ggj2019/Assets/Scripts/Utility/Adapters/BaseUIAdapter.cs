using UnityEngine; 

public abstract class BaseUIAdapter : MonoBehaviour
{
	/// <summary>
	/// Was the value field in the inspector set? 
	/// </summary>
	private bool valueFieldSet;

	protected abstract BaseValue Value
	{
		get;
	}

	protected virtual void Awake()
	{
		valueFieldSet = Value != null;

		if (!valueFieldSet)
			return;

		Value.AddObserver(OnValueChanged);
	}

	protected abstract void OnValueChanged(BaseValue value);

	protected virtual void OnDestroy()
	{
		if (valueFieldSet)
			Value.RemoveObserver(OnValueChanged);
	}
}
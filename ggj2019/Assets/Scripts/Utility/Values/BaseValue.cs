using UnityEngine;

public abstract class BaseValue : ScriptableObject
{
	public delegate void ValueDataUpdated(BaseValue setting);

	private event ValueDataUpdated ValueChangedEvent;

	public void AddObserver(ValueDataUpdated update)
	{
		ValueChangedEvent += update;
	}

	public void RemoveObserver(ValueDataUpdated update)
	{
		ValueChangedEvent -= update;
	}

	public void ValueUpdated()
	{
		if (ValueChangedEvent != null)
			ValueChangedEvent(this);
	}

	public abstract object ToObject();

	public abstract bool FromObject(object value);
}

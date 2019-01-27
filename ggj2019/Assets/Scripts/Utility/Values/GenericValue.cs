using UnityEngine;
using System;

public class GenericValue<T> : BaseValue where T : IComparable
{
	[HideInInspector]
	public T defaultValue = default(T);

	[SerializeField]
	protected T value;

	private void OnEnable()
	{
		value = defaultValue;
	}

	protected virtual bool Equals(T other)
	{
		return (other.Equals(this.value));
	}

	public override string ToString()
	{
		return value.ToString();
	}

	/// <summary>
	/// Value interface
	/// </summary>
	public virtual T Value
	{
		get { return value; }
		set
		{
			bool equals = Equals(value);

			this.value = value;

			ValueUpdated();
		}
	}

	public override object ToObject()
	{
		return Value;
	}

	public override bool FromObject(object value)
	{
		try
		{
			this.value = (T)value;
		}
		catch (Exception e)
		{
			Debug.LogError("Unable to cast object(" + value.ToString() + ") to type " + typeof(T).Name + "\r\n" + e.Message);
			return false;
		}

		return true;
	}
}

using UnityEngine;

[CreateAssetMenu(fileName = "New StringValue", menuName = "BaseValues/StringValue")]
public class StringValue : GenericValue<string>
{
	protected override bool Equals(string other)
	{
		return value == other;
	}
}

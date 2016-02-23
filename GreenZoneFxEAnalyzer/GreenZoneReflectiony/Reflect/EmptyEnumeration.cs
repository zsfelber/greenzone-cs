using System;

namespace GreenZoneParser.Reflect
{
	[Flags]
	public enum BindingFlags
	{
		Default = 0,
		DeclaredOnly = 2,
		ExactBinding = 65536,
		FlattenHierarchy = 64,
		IgnoreCase = 1,
		Instance = 4,
		NonPublic = 32,
		OptionalParamBinding = 262144,
		Public = 16,
		Static = 8
	}
}


using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;


[Serializable]
public abstract class VariableConfig : BaseConfig, ICopiable<VariableConfig>
{

    private ObjSerializableDictionary variables;


	public VariableConfig()
	{
        variables = new ObjSerializableDictionary();
	}

    public virtual void Copy(VariableConfig other, CopyMode copyMode = CopyMode.ALL)
	{
        ReplaceVariables(other.variables, copyMode);
	}

    public ObjSerializableDictionary Variables
	{
		get
		{
			return variables;
		}
		//set
		//	variables = value;
		//}
	}

    public void ClearVariables()
	{
		ClearXXX(variables, "Variables");
	}

    public void ReplaceVariables(Dictionary<Object, Object> _variables, CopyMode copyMode)
	{
		ReplaceXXX(variables, "Variables", _variables, copyMode);
	}

    public void SetVariable(Object key, Object value, CopyMode copyMode)
	{
        SetXXX(variables, "Variables", key, value, copyMode);
	}

    public void RemoveVariable(Object key)
	{
		RemoveXXX(variables, "Variables", key);
	}

}
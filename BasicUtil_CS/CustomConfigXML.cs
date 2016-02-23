using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


[Serializable]
public abstract class CustomConfigXML : VariableConfig
{

    public CustomConfigXML()
    {
    }

    public void Serialize(string file)
    {
        XmlSerializer xs = new XmlSerializer(GetType());
        StreamWriter writer = File.CreateText(file);
        xs.Serialize(writer, this);
        writer.Flush();
        writer.Close();
    }

    public static CustomConfigXML Deserialize(string file, Type type)
	{
        EALogger.Log("CustomConfigXML.Deserialize  file:" + file+"  type:"+type.Name, EALogger.SEV_INFO);
		XmlSerializer xs = new XmlSerializer(type);
		StreamReader reader = File.OpenText(file);
		CustomConfigXML c = (CustomConfigXML) xs.Deserialize(reader);
		reader.Close();
		return c;
	}

}
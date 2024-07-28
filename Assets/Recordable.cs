using System.Xml;

public interface IXmlRecordable
{
    public void RecordToElement(XmlDocument document, XmlElement element);
    public void InitFromElement(XmlElement element);
}

public interface IXmlRecordableTransform : IXmlRecordable
{
    public string GetId();
    public string GetPrefab();
}
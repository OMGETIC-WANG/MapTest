using System.Xml;
using System.Xml.Schema;
using Unity.Mathematics;
using UnityEngine;

public class RecordableTransform : MonoBehaviour, IXmlRecordableTransform
{
    public string Id = "null";
    public void RecordToElement(XmlDocument document, XmlElement element)
    {
        XmlElement pos = XmlHelper.Vector3ToXmlElem(document, transform.position, "Position");
        element.AppendChild(pos);
        XmlElement rotation = XmlHelper.Vector3ToXmlElem(document, transform.rotation.eulerAngles, "Rotation");
        element.AppendChild(rotation);
        XmlElement scale = XmlHelper.Vector3ToXmlElem(document, transform.localScale, "Scale");
        element.AppendChild(scale);
        XmlElement id = document.CreateElement("Id");
        id.SetAttribute("value", $"{Id}");
        element.AppendChild(id);
    }
    public void InitFromElement(XmlElement element)
    {
        transform.position = XmlHelper.XmlElemToVector3((XmlElement)element.SelectSingleNode("Position"));
        transform.rotation = Quaternion.Euler(XmlHelper.XmlElemToVector3((XmlElement)element.SelectSingleNode("Rotation")));
        transform.localScale = XmlHelper.XmlElemToVector3((XmlElement)element.SelectSingleNode("Scale"));
        Id = ((XmlElement)element.SelectSingleNode("Id")).GetAttribute("value");
    }
    public string GetId()
    {
        return Id;
    }
    public string GetPrefab()
    {
        var prefabAsset = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
        return UnityEditor.AssetDatabase.GetAssetPath(prefabAsset);
    }
}
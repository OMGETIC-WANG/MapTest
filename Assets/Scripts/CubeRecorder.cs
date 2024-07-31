using System.ComponentModel.Design;
using System.Xml;
using UnityEngine;

public class CubeRecorder : MonoBehaviour, IXmlRecordable
{
    public Color BaseColor;

    public void RecordToElement(XmlDocument document, XmlElement element)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.GetPropertyBlock(props);
        var baseColorElem = XmlHelper.ColorToXmlElem(document, BaseColor, "BaseColor");
        element.AppendChild(baseColorElem);
    }
    public void InitFromElement(XmlElement element)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        BaseColor = XmlHelper.XmlElemToColor((XmlElement)element.SelectSingleNode("BaseColor"));
        props.SetColor("_BaseColor", BaseColor);
        renderer.SetPropertyBlock(props);
    }
}

using UnityEngine;
using System.Xml;

public class XmlHelper
{
    public static XmlDocument InitXmlDoc()
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
        doc.AppendChild(decl);
        return doc;
    }
    public static XmlElement Vector3ToXmlElem(XmlDocument document, Vector3 vec, string name)
    {
        XmlElement elem = document.CreateElement(name);
        elem.SetAttribute("X", $"{vec.x}");
        elem.SetAttribute("Y", $"{vec.y}");
        elem.SetAttribute("Z", $"{vec.z}");
        return elem;
    }
    public static Vector3 XmlElemToVector3(XmlElement element)
    {
        Vector3 vec = new Vector3();
        vec.x = float.Parse(element.GetAttribute("X"));
        vec.y = float.Parse(element.GetAttribute("Y"));
        vec.z = float.Parse(element.GetAttribute("Z"));
        return vec;
    }
    public static XmlElement ColorToXmlElem(XmlDocument document, Color color, string name)
    {
        XmlElement elem = document.CreateElement(name);
        elem.SetAttribute("R", $"{color.r}");
        elem.SetAttribute("G", $"{color.g}");
        elem.SetAttribute("B", $"{color.b}");
        elem.SetAttribute("A", $"{color.a}");
        return elem;
    }
    public static Color XmlElemToColor(XmlElement element)
    {
        Color vec = new Color();
        vec.r = float.Parse(element.GetAttribute("R"));
        vec.g = float.Parse(element.GetAttribute("G"));
        vec.b = float.Parse(element.GetAttribute("B"));
        vec.a = float.Parse(element.GetAttribute("A"));
        return vec;
    }
}
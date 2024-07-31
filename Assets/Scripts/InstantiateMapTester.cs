using System.Xml;
using UnityEngine;

public class InstantiateMapTester : MonoBehaviour
{
    public string XmlPath;
    void Start()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(XmlPath);
        InstantiateMap.Instantiate(doc);
    }
}
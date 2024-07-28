using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class MapRecorder : ScriptableWizard
{
    public string RecordPath;
    [MenuItem("Map/Record")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<MapRecorder>("Record", "Apply");
    }
    void OnWizardCreate()
    {
        XmlDocument doc = XmlHelper.InitXmlDoc();
        XmlElement root = doc.CreateElement("Root");
        doc.AppendChild(root);
        GameObject[] objects = FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None) as GameObject[];
        foreach (var obj in objects)
        {
            IXmlRecordable[] recordables = obj.GetComponents<IXmlRecordable>();
            IXmlRecordableTransform rTransform = obj.GetComponent<IXmlRecordableTransform>();
            if (recordables.Length > 0)
            {
                XmlElement objElem = doc.CreateElement("GameObject");
                objElem.SetAttribute("Prefab", rTransform.GetPrefab());
                root.AppendChild(objElem);
                foreach (var comp in recordables)
                {
                    XmlElement compElem = doc.CreateElement(comp.GetType().Name);
                    comp.RecordToElement(doc, compElem);
                    objElem.AppendChild(compElem);
                }
            }
        }
        SafeSave(doc);
    }
    void SafeSave(XmlDocument doc)
    {
        string file = Path.GetFileName(RecordPath);
        string dir = Path.GetDirectoryName(RecordPath);
        if (Path.GetExtension(file) != ".xml")
        {
            file += ".xml";
        }
        string finalPath = Path.Combine(dir, file);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            doc.Save(finalPath);
            return;
        }
        if (File.Exists(finalPath))
        {
            File.Delete(finalPath);
        }
        doc.Save(finalPath);
    }
}
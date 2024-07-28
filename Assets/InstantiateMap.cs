using UnityEngine;
using System.Xml;
using System.Collections.Generic;
public class InstantiateMap
{
    public static void Instantiate(XmlDocument document)
    {
        XmlElement root = (XmlElement)document.SelectSingleNode("Root");
        foreach (XmlElement objElem in root.ChildNodes)
        {
            GameObject obj = GameObject.Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(objElem.GetAttribute("Prefab")));
            Dictionary<string, List<IXmlRecordable>> dic = new Dictionary<string, List<IXmlRecordable>>();
            SortRecordableComponents(obj, dic);
            foreach (XmlElement elem in objElem.ChildNodes)
            {
                if (dic.TryGetValue(elem.Name, out var list))
                {
                    list[list.Count - 1].InitFromElement(elem);
                    list.RemoveAt(list.Count - 1);
                    if (list.Count == 0)
                    {
                        dic.Remove(elem.Name);
                    }
                }
                else
                {
                    IXmlRecordable comp = (IXmlRecordable)obj.AddComponent(ReflHelper.GetTypeByName(elem.Name));
                    comp.InitFromElement(elem);
                }
            }
            foreach (var pair in dic)
            {
                foreach (var comp in pair.Value)
                {
                    Object.Destroy((Component)comp);
                }
            }
        }
    }
    public static void SortRecordableComponents(GameObject obj, Dictionary<string, List<IXmlRecordable>> dic)
    {
        foreach (var comp in obj.GetComponents<IXmlRecordable>())
        {
            string name = comp.GetType().Name;
            if (dic.TryGetValue(name, out var compList))
            {
                compList.Add(comp);
            }
            else
            {
                dic.Add(name, new List<IXmlRecordable> { comp });
            }
        }
    }
}
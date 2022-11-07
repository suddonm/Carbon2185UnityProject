using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;

public class BaseNode : Node
{
    public string nodeGuid;

    public BaseNode()
    {
        //styleSheets.Add(Resources.Load<StyleSheet>("Node"));
        AddToClassList("node");
    }
}

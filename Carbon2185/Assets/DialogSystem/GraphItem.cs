using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GraphItem : ScriptableObject
{
    public List<NodeLinkData> nodeLinks = new List<NodeLinkData>();
    public List<NodeData> nodeData = new List<NodeData>();

}

[System.Serializable]
public class NodeLinkData
{
    public string baseNodeGuid;
    public string outputPortName;
    public string inputPortName;
    public string targetNodeGuid;
}

[System.Serializable]
public class NodeData
{
    public string nodeGuid;
    public Vector2 position;
    public string nodeType;
    public string additionalDataJSON;
}
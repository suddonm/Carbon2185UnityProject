using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;

public class DialogSystemEditor : EditorWindow
{
    DialogGraphView graphView;

    [MenuItem("Dialog System/Open")]
    public static void DialogSystemEditorWindow()
    {
        GetWindow<DialogSystemEditor>("Dialog System Editor Window");
    }

    void OnEnable()
    {
        graphView = new DialogGraphView();
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

    void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }
}

public class DialogGraphView : GraphView
{
    public DialogGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();        
    }
}
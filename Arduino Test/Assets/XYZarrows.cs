using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZarrows : MonoBehaviour
{

    Material lineMaterial;

    void Start()
    {
        lineMaterial = new Material(Shader.Find("MyGizmoLines"));
        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
    }

    void OnRenderObject()
    {
        Vector2 origin = new Vector2(0.5f, 0.9f);
        Vector3 pos = Camera.main.ViewportToWorldPoint(origin);
        float length = 0.1f;
        GL.PushMatrix();

        lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);

        GL.Color(Color.red);
        GL.Vertex(pos);
        GL.Vertex(pos + Vector3.right * length);

        GL.Color(Color.yellow);
        GL.Vertex(pos);
        GL.Vertex(pos + Vector3.up * length);

        GL.Color(Color.blue);
        GL.Vertex(pos);
        GL.Vertex(pos + Vector3.forward * length);

        GL.End();
        GL.PopMatrix();
    }


}


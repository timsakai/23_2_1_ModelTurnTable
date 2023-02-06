using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PawnGenerator : MonoBehaviour
{
    [SerializeField] Transform pawnParent;
    [SerializeField] Transform pedestalParent;
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] Transform basePoint;
    [SerializeField] GameObject pedestal;
    [SerializeField] float radius = 10f;
    [SerializeField] List<GameObject> generated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#if UNITY_EDITOR
    [ContextMenu("Generate")]
    public void Generate()
    {
        foreach (var item in generated)
        {
            DestroyImmediate(item.gameObject);
        }
        generated.Clear();
        Vector3 pos = new Vector3(0, 0, radius);
        for (int i = 0; i < prefabs.Count; i++)
        {
            List<GameObject> objs = new List<GameObject>();
            objs.Add((GameObject)PrefabUtility.InstantiatePrefab(prefabs[i]));
            if(pedestal != null) objs.Add((GameObject)PrefabUtility.InstantiatePrefab(pedestal));
            pos = Quaternion.Euler(Vector3.down * (360 / prefabs.Count)) * pos;
            for (int ii = 0; ii < objs.Count; ii++)
            {
                objs[ii].transform.rotation = Quaternion.Euler(Vector3.down * ((360 / prefabs.Count) * i + 90));
                objs[ii].transform.position = pos + transform.position;
                if (ii == 0)
                {
                    objs[ii].transform.Translate(basePoint.localPosition);
                    objs[ii].transform.SetParent(pawnParent);
                }
                else
                {
                    objs[ii].transform.SetParent(pedestalParent);
                }
                generated.Add(objs[ii]);

            }
        }
        gameObject.GetComponent<FocusManager>().SetPawns(generated);
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalController : MonoBehaviour
{
    [SerializeField] private int n = 2;
    [SerializeField] private GameObject root;
    public float branchLengthScale = 0.5f; // Scale factor for branch length
    public float branchAngle = 20f;
    [SerializeField] private GameObject line;
    private List<GameObject> fractalTree = new List<GameObject>();
    //colorsArray[colorsArray.Length - 1] = "Orange";
    void Start(){
        root.transform.GetChild(1).GetChild(0).transform.position = new Vector3(0f,-4.96f,0f);
        root.transform.GetChild(1).GetChild(1).transform.position = new Vector3(0f,-4.28f,0f);

        // StartBranch();
    }

    public void StartBranch(){
        CreateBranch(root.transform.GetChild(1).GetChild(1).transform, branchLengthScale, branchAngle, 0);
        CreateBranch(root.transform.GetChild(1).GetChild(1).transform, branchLengthScale, -branchAngle, 0);
    }
    public void DestroyTree(){
        if(fractalTree.Count == 0) return;
        for (int i = 0; i < fractalTree.Count; i++){
            Destroy(fractalTree[i]);
        }
    }
    private void CreateBranch(Transform startPoint, float lengthScale, float angle, int depth){
        if(depth>=n)
            return;
        //create the prefab
        GameObject branch = Instantiate(line, new Vector3(0f,0f,0f), Quaternion.identity ) as GameObject;
        fractalTree.Add(branch);
        //starting point of the line
        branch.transform.GetChild(1).GetChild(0).transform.position = startPoint.position;
        Vector3 direction = Quaternion.Euler(0f, 0f, angle) * (startPoint.position - root.transform.GetChild(1).GetChild(0).transform.position);
        //ending point of the line
        branch.transform.GetChild(1).GetChild(1).transform.position = startPoint.position + direction * lengthScale;

        CreateBranch(branch.transform.GetChild(1).GetChild(1).transform, lengthScale, angle, depth + 1);
        CreateBranch(branch.transform.GetChild(1).GetChild(1).transform, lengthScale, -angle, depth + 1);
    }
}

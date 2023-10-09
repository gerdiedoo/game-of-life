using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FractaUI : MonoBehaviour
{
    [SerializeField] private Slider angle;
    [SerializeField] private Slider length;
    [SerializeField] private TextMeshProUGUI angleTMP;
    [SerializeField] private TextMeshProUGUI lengthTMP;
    [SerializeField] private Button apply;
    private FractalController fcVar;
    // Start is called before the first frame update
    void Start()
    {
        fcVar = FindObjectOfType<FractalController>();
        angle.value = fcVar.branchAngle;
        length.value = fcVar.branchLengthScale;
        angleTMP.text = angle.value.ToString();
        lengthTMP.text = length.value.ToString();

        apply.onClick.AddListener(applyTask); 
    }

    public void applyTask(){
        fcVar.DestroyTree();
        fcVar.StartBranch();
    }
    // Update is called once per frame
    void Update()
    {
        fcVar.branchAngle = angle.value;
        fcVar.branchLengthScale =  length.value;
        angleTMP.text = angle.value.ToString();
        lengthTMP.text = length.value.ToString();
    }
}

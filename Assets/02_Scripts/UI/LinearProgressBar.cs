using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LinearProgressBar : MonoBehaviour{
#if UNITY_EDITOR
    [MenuItem("GameObject/UICharInfo/LinearProgressBar Progress Bar")]
    public static void AddLinearProgressBar(){
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform,false);
    }
#endif

    public float minimum;
    public float maximum;
    public float current;
    public Image mask;
    public Image fill;
    public Color color;
    void Start(){
        
    }

    void Update(){
        
    }

    public void GetCurrentFill(){
        float currentOffSet = current - minimum;
        float maximumOffSet = maximum - minimum;
        float fillAmount = currentOffSet / maximumOffSet;
        mask.fillAmount = fillAmount;
        fill.color = color;
    }
}

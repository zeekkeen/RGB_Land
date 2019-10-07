using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour{
    GameObject crystal;
    public bool painted = true, changeColor = false;
    public Gradient gradient;
    public TypeOfColor typeOfColor = TypeOfColor.red;
    public float timeToPaint = 1f;
    public enum TypeOfColor{
        red,
        green,
        blue
    }
     public GameObject particlesTrail;
     public List <SpriteRenderer> colorRender = new List<SpriteRenderer>{};

    void Start(){
        crystal = GameObject.FindGameObjectWithTag("Crystal");
    }

    // Update is called once per frame
    void Update(){
        if(changeColor){
            if(painted){
                    timeToPaint -= Time.deltaTime;
                    if(timeToPaint <= 0){
                        changeColor = false;
                        painted = false;
                        timeToPaint = 0;
                    }
            }else{
                
                    timeToPaint += Time.deltaTime;
                    if(timeToPaint >= 1){
                        changeColor = false;
                        painted = true;
                        timeToPaint = 1;
                    }
                
            }
            foreach (SpriteRenderer element in colorRender){
                switch (typeOfColor){
                    case TypeOfColor.red:
                        element.color = new Color(timeToPaint, element.color.g, element.color.b, 1);
                        break;
                    case TypeOfColor.green:
                        element.color = new Color(element.color.r, timeToPaint, element.color.b, 1);
                        break;
                    case TypeOfColor.blue:
                        element.color = new Color(element.color.r, element.color.g, timeToPaint, 1);
                        break;
                }
            }
        }
    }

    public void SwitchColor(){
        GameObject trail = (GameObject)Instantiate(particlesTrail,(painted?transform.position:crystal.transform.position),Quaternion.identity);
        trail.GetComponent<TrailMovement>().StartMovement((painted?crystal.transform:transform),gradient);
        changeColor = true;
    }
}

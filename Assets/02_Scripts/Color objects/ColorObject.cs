using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour{
    GameObject crystal;
    public bool painted, changeColor;
    public Gradient gradient;
    public TypeOfColor typeOfColor = TypeOfColor.red;
    public float timeToPaint = 0f;
     public GameObject particlesTrail;
     public List <SpriteRenderer> colorRender = new List<SpriteRenderer>{};
    public enum TypeOfColor{
        red,
        green,
        blue
    }

    void Start(){
        crystal = GameObject.FindGameObjectWithTag("Crystal");
        foreach (SpriteRenderer element in colorRender){
                switch (typeOfColor){
                    case TypeOfColor.red:
                        element.color = new Color(1, 0, 0);
                        break;
                    case TypeOfColor.green:
                        element.color = new Color(0, 1, 0);
                        break;
                    case TypeOfColor.blue:
                        element.color = new Color(0, 0, 1);
                        break;
                }
        }
        if(painted)
            timeToPaint = 0;
        else 
            timeToPaint = 1f;
        changeColor = false;
    }

    // Update is called once per frame
    void Update(){
        if(changeColor){
            if(painted){
                timeToPaint += Time.deltaTime;
                if(timeToPaint >= 1){
                    changeColor = false;
                    painted = false;
                    timeToPaint = 1;
                }
            }else{
                timeToPaint -= Time.deltaTime;
                if(timeToPaint <= 0){
                    changeColor = false;
                    painted = true;
                    timeToPaint = 0;
                }
            }
            foreach (SpriteRenderer element in colorRender){
                switch (typeOfColor){
                    case TypeOfColor.red:
                        element.color = new Color(1, timeToPaint, timeToPaint);
                        break;
                    case TypeOfColor.green:
                        element.color = new Color(timeToPaint, 1, timeToPaint);
                        break;
                    case TypeOfColor.blue:
                        element.color = new Color(timeToPaint, timeToPaint, 1);
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

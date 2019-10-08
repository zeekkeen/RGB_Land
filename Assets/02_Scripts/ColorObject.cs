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
                        element.color = new Color(element.color.r, timeToPaint, timeToPaint);
                        break;
                    case TypeOfColor.green:
                        element.color = new Color(timeToPaint, element.color.g, timeToPaint);
                        break;
                    case TypeOfColor.blue:
                        element.color = new Color(timeToPaint, timeToPaint, element.color.b);
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

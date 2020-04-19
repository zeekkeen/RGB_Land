using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlueBossManager : MonoBehaviour
{
    public IABlueBoss blueBoss;
    public List<GameObject> principalPlataforms;

    [Header("Rutas")]
    public List<GameObject> rutas;
    public GameObject rutaCenter;


    [Header("Proyectiles")]
    public List<GameObject> proyectilesSpawns;
    public GameObject Proyectil;

    void Start()
    {
        
    }
    public void SpawnPlataforms(int dir)
    {
        for (int i = 0; i < principalPlataforms.Count; i++)
        {
            if(principalPlataforms[i].activeSelf)
            {
                GameObject ruta;
                if (i==1)
                    ruta=rutaCenter;
                else
                    ruta=rutas[Random.Range(0,1)];

                ruta.SetActive(true);
                ruta.transform.localScale=new Vector3(dir,1,1);
                break;
            }    
        }
    }

    public void CreatePatron(List<int> patron){
        for (int i = 0; i < proyectilesSpawns.Count; i++)
        {
            if(!patron.Contains(i))
            {
                Transform spawn = proyectilesSpawns[i].transform;
                Instantiate(Proyectil,spawn.position,spawn.rotation);
            }
                
        }
    }
    public void DesactivePlataforms(){
        foreach (GameObject plat in principalPlataforms.Union(rutas))
        {
            plat.SetActive(false);
        }
        rutaCenter.SetActive(false);
    }

    public void BlueBossTrasport(int value)
    {
        GameObject plataform;
        for (int i = 0; i < principalPlataforms.Count; i++)
        {
            plataform = principalPlataforms[i];
            if (i==value)
            {
                plataform.SetActive(true);
                blueBoss.transform.position=plataform.transform.GetChild(0).position;
            }
        }
    }

    void Update()
    {
        
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour{
    
    public Animator transitionAnim;
    public static TransitionManager instance;

    string sceneNameToLoad;

    void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        // DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
    }

    public void LoadSceneWithTransition(string name){
        sceneNameToLoad = name;
        StartCoroutine(LoadScene());
    }
    
    IEnumerator LoadScene(){
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
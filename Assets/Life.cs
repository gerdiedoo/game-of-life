using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public enum state{
    unknown,
    dead,
    alive
}
public class Life : MonoBehaviour
{
    // Start is called before the first frame update
    public int x;
    public int y;
    public state lifeState = state.unknown;
    public int neighbors = 0;
    public int amountDeath = 0;
    public int amountLife = 0;
    private conwayLife conwayLifeVar;
    private SpriteRenderer spriteRenderer;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        conwayLifeVar = FindObjectOfType<conwayLife>();
        UpdateColor();
    }

    public void UpdateColor(){
        if(lifeState == state.alive){
            spriteRenderer.color = Color.white;
            amountLife++;
        }else if(lifeState == state.dead){
            spriteRenderer.color = Color.black;
            amountDeath++;
        }
    }

}

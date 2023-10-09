using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class conwayLife : MonoBehaviour
{

    public TextMeshProUGUI gen;
    public int width = 10;
    public int height = 10;
    public GameObject tile;
    public GameObject lifeGo;
    public int generation=0;
    private int offset = 20;
    private bool shouldWait = true;
    public GameObject[,] lives;
    public int[,] next;
    public void Start(){
        lives =  new GameObject[width, height];
        
        initialize();
        initializeRandom();
        StartCoroutine(StartSimulation());
    }

    
    private void initialize(){
        for(int i = 0; i < width ; ++i){
            for(int j = 0 ; j < height ; ++j){
                Vector2 tempPosition = new Vector2(i,j);
                Vector2 initialPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tile, initialPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent =  this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";
                GameObject life = Instantiate(lifeGo, tempPosition, Quaternion.identity);
                life.GetComponent<Life>().x = j;
                life.GetComponent<Life>().y = i;
                life.transform.parent =  this.transform;
                life.name =  "( " + i + ", " + j + " )";
                lives[i, j] = life;
            }
        }
    }
    private void initializeDeath(){
        for(int i = 0; i < width ; i++) {
            for(int j = 0 ; j < height ; j++){
                lives[i,j].GetComponent<Life>().lifeState = state.dead;
            }
        }
    }
    private void initializeLife(){
        for(int i = 0; i < width ; i++) {
            for(int j = 0 ; j < height ; j++){
                lives[i,j].GetComponent<Life>().lifeState = state.alive;
            }
        }
    }
    private void initializeRandom(){
        for(int i = 0; i < width ; i++) {
            for(int j = 0 ; j < height ; j++){
                lives[i,j].GetComponent<Life>().lifeState = (Random.value < 0.5f) ? state.alive : state.dead;
            }
        }
        UpdateNeighbors();
    }
    public void UpdateNeighbors(){
        for(int i = 0; i < width ; i++) {
            for(int j = 0 ; j < height ; j++){
                lives[i,j].GetComponent<Life>().neighbors = CountAdjacentLives(i,j);
            }
        }
    }
    private int CountAdjacentLives(int x, int y)
    {
        int count = 0;
        int rows = lives.GetLength(0);
        int columns = lives.GetLength(1);

        // Check adjacent cells in a 3x3 window centered at (x, y)
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newX = x + i;
                int newY = y + j;

                // Skip the cell at (x, y) itself
                if (i == 0 && j == 0)
                    continue;

                // Skip out-of-bounds coordinates
                if (newX < 0 || newX >= rows || newY < 0 || newY >= columns)
                    continue;

                // Count adjacent ones
                if (lives[newX, newY].GetComponent<Life>().lifeState == state.alive)
                    count++;
            }
        }

        return count;
    }

    IEnumerator StartSimulation()
    {
        while (shouldWait)
        {
            yield return new WaitForSeconds(0.1f);
            Simulate();
        }

        Debug.Log("Coroutine finished.");
    }
    // Any live cell with fewer than two live neighbours dies, as if by underpopulation.
    // Any live cell with two or three live neighbours lives on to the next generation.
    // Any live cell with more than three live neighbours dies, as if by overpopulation.
    // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

    private void ComputeNextGeneration(){
        next = new int[width, height];
        for(int i=0;i<width;i++){
            for(int j=0;j<height;j++){
                Life temp_life = lives[i, j].GetComponent<Life>();
                temp_life.neighbors = CountAdjacentLives(i,j);
                if(temp_life.lifeState == state.dead && temp_life.neighbors ==3){
                    next[i,j] = 1;
                }else if (temp_life.lifeState == state.alive && ( temp_life.neighbors < 2 || temp_life.neighbors > 3)){
                    next[i,j] = 0;
                }else {
                    next[i,j] = temp_life.lifeState == state.dead ? 0 : 1;
                }
            }
        }
    }

    private void Simulate(){
        //calculate the next generation
        ComputeNextGeneration();
        //update the board
        
        for(int i=0;i< width;i++){
            for(int j=0;j<height;j++){
                Life temp_life = lives[i,j].GetComponent<Life>();
                temp_life.lifeState = next[i,j] == 1 ? state.alive : state.dead; 
                temp_life.UpdateColor();
            }
        }
        generation++;
        gen.text = generation.ToString();
    }
    
}
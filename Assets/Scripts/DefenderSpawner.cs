using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;
    Defender[,] defenderTilesOccupation;
    [SerializeField] float sellFactor = 0.5f;
    private void Start()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        defenderTilesOccupation = new Defender[(int)box.size.x,(int)box.size.y];
    }
    public bool tileIsEmpty(Vector2 v)
    {
        return (!this.defenderTilesOccupation[(int)v.x -1,(int)v.y -1]);
    }
    public void setTileOccupation(Vector2 v,Defender defender)
    {
        this.defenderTilesOccupation[(int)v.x -1,(int)v.y -1] = defender;
    }
    public Defender getDefender(Vector2 pos)
    {
        return this.defenderTilesOccupation[(int)pos.x - 1, (int)pos.y - 1];
    }
    public float getSellFactor()
    {
        return sellFactor;
    }
    private void OnMouseDown()
    {
        

    SpawnDefender(( GetSquareClicked()));
    }
   
    private void SpawnDefender(Vector2 coordinate)
    {
      //  Defender defender = Instantiate(this.defender, coordinate, Quaternion.identity);
    }
    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        worldPos = SnapToGrid(worldPos);
        return  worldPos;
    }
    public static Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        return new Vector2(newX, newY);
    }
    public void SetSelectedDefender(Defender defender)
    {
        this.defender = defender;
    }
}

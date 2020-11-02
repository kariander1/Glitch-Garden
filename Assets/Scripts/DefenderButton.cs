using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] bool sellButton = false;
    [SerializeField] Defender defenderPrefab;
    private GameObject draggedObject;
    private bool dragged = false;
    private bool outOfBounds = true;
    private Vector2 currentSpawnPos;

    private void OnMouseEnter()
    {
        RevealImage();   
    }
    private void OnMouseExit()
    {
        if (!dragged)
        {
            RestoreColor();
            FindObjectOfType<CostDisplay>().HidePrice();
        }
    }
    private void RevealImage()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.white;
        CostDisplay costDisplay = FindObjectOfType<CostDisplay>();

        costDisplay.SetCost(defenderPrefab.GetCost());
        costDisplay.RevealPrice();
    }
    private void OnMouseDown()
    {
        RevealImage();

        draggedObject = Instantiate(gameObject, transform.position,Quaternion.identity);
        
        draggedObject.GetComponent<DefenderButton>().SetDragged(true);
      //  draggedObject.transform.localScale = new Vector3(1, 1, 1);
        draggedObject.transform.localScale = transform.lossyScale;
        //   draggedObject.transform.Translate(0, 0, 10);
        dragged = true;

        //FindObjectOfType<DefenderSpawner>().SetSelectedDefender(this.defenderPrefab);
    }
    private void OnMouseUp()
    {
        dragged = false;
        AttempToPlaceDefender();
        Destroy(draggedObject);
        RestoreColor();
    }
    public void SetDragged(bool dragged)
    {
        this.dragged = dragged;
    }
    private void Spawn()
    {
        DefenderSpawner dsp = FindObjectOfType<DefenderSpawner>();
        
        Defender defender = Instantiate(this.defenderPrefab, currentSpawnPos, Quaternion.identity);
        dsp.setTileOccupation(currentSpawnPos, defender);
    }
    public static Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        return new Vector2(newX, newY);
    }
    private void RestoreColor()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color toChange;
        ColorUtility.TryParseHtmlString("#333333", out toChange);
        renderer.color = toChange;
    }
    private void Update()
    {
        if (dragged && draggedObject)
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            DefenderSpawner dsp = FindObjectOfType<DefenderSpawner>();
                Vector2 realPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 tempPos = SnapToGrid(realPos);
            if (( dsp.GetComponent<BoxCollider2D>().bounds.Contains(realPos) ))
            {
                if (dsp.tileIsEmpty(tempPos) || sellButton)
                {
                    outOfBounds = false;

                    currentSpawnPos = tempPos;
                    draggedObject.transform.position = currentSpawnPos;
                }
            }
            else
                outOfBounds = true;

        }
    }
    private void AttempToPlaceDefender()
    {
        if (!outOfBounds)
        {
            DefenderSpawner dsp = FindObjectOfType<DefenderSpawner>();
            var CoinDisplay = FindObjectOfType<CoinDisplay>();
            if(sellButton)
            {
                Defender placedDefender = dsp.getDefender(currentSpawnPos);
                if (placedDefender)
                {
                    int defenderCost = placedDefender.GetCost();
                    CoinDisplay.Addcoins((int)(defenderCost *dsp.getSellFactor()));
                    Destroy(placedDefender.gameObject);
                }
            }
            else
            {
                int defenderCost = defenderPrefab.GetCost();
                if (CoinDisplay.Spendcoins(defenderCost))
                {
                    Spawn();
                }
            }

        }
    }
}

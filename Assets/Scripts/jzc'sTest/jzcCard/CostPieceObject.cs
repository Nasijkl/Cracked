using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostPieceObject : CardPieceObject
{
    public int cost;

    [SerializeField] private TextMeshPro costText;

    public override int pieceType(){
        return 1;
    }

    public override void initialize(CardPieceData data){
        CostPieceData cost_data = data as CostPieceData;
        this.sprite = cost_data.sprite;
        this.cost = cost_data.cost;

        SpriteRenderer spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    void OnTransformChanged()
    {
        // 进行输出或处理
        Debug.Log("Transform changed: " + transform.position + ", " + transform.rotation);
    }
}

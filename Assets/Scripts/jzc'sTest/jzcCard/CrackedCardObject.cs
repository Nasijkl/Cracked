using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CrackedCardObject : MonoBehaviour
{
    [SerializeField]public GameObject[] card_pieces  = new GameObject[4];
    
    public CrackedCardData data;
    //public RuntimeCard runtimeCard;

    private Vector3 _savedPosition;
    private Quaternion _savedRotation;
    private int _savedSortingOrder;

    private SortingGroup _sortingGroup;

    public enum CardState
    {
        InHand, // 手握牌状态
        AboutToBePlayed // 待出牌状态
    }

    private CardState _currentState;
    public CardState State => _currentState;

    private void Start()
    {
        //card_pieces;
        for(int i=0;i<4;i++)
        {
            //card_pieces[i] = null;
        }
    }

    private void OnEnable()
    {
        SetState(CardState.InHand);
    }

    public void SetState(CardState state)
    {
        _currentState = state;
    }

    private void Awake()
    {
        _sortingGroup = GetComponent<SortingGroup>();
    }

    // private void Start()
    // {
    //     var testCard = new RuntimeCard
    //     {
    //         Template = template
    //     };
    //     SetInfo(testCard);
    // }

    public void SetInfo(CrackedCardData card)
    {
        data = card;
        //Debug.Log("set cracked card info");
        for(int i = 0; i < 4; i++)
        {
            //Debug.Log("piece_"+i);
            //Debug.Log(data.card_pieces[i]);
            if(data.card_pieces[i]!=null)
            {
                GameObject child_object = new GameObject("card_piece_" + i);
                Debug.Log(child_object.transform.position);
                child_object.transform.parent = transform;
                Debug.Log(child_object.transform.position);
                child_object.transform.localPosition = new Vector3(0.16f,0.05f,0);
                child_object.transform.localScale = Vector3.one;
                Debug.Log("setinfo");
                
                CardPieceObject piece;
                if(data.card_pieces[i] is CostPieceData)
                {
                    piece = child_object.AddComponent<CostPieceObject>();
                }
                else if(data.card_pieces[i] is LabelPieceData)
                {
                    piece = child_object.AddComponent<LabelPieceObject>();
                }
                else if(data.card_pieces[i] is EffectPieceData && i == 2)
                {
                    piece = child_object.AddComponent<LeftEffectPieceObject>();
                }
                else // if(data.card_pieces[i] is CostPieceData && i ==3)
                {
                    piece = child_object.AddComponent<RightEffectPieceObject>();
                }
                piece.initialize(data.card_pieces[i]);
            }
        }
        //template = card.Template;
        //costText.text = template.Cost.ToString();
        //nameText.text = template.Name;
        //typeText.text = template.Type.TypeName;
        var builder = new StringBuilder();
        //descriptionText.text = builder.ToString();
        //picture.sprite = template.Picture;
    }

    public void SaveTransform(Vector3 position, Quaternion rotation)
    {
        _savedPosition = position;
        _savedRotation = rotation;
        _savedSortingOrder = _sortingGroup.sortingOrder;
    }
    

    public void Reset(Action onComplete)
    {

        Transform childObject = transform.GetChild(0) ;
        // 在这里对每个子对象进行操作
        Debug.Log("Child Object: {childObject.name}");
        Debug.Log(childObject.position);

        //Debug.Log(child_object.transform.position);
        transform.DOMove(_savedPosition, 0.2f);
        transform.DORotateQuaternion(_savedRotation, 0.2f);
        _sortingGroup.sortingOrder = _savedSortingOrder;

        Debug.Log(childObject.position);

        onComplete();
    }
}

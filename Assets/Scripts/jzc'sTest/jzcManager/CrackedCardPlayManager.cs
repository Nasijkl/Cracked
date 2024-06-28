using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class CrackedCardPlayManager : CardSelectionBase
{
    private Vector3 originalCardPosition;
    private Vector3 aboutToPlayPosition;
    private Quaternion originalCardRotation;
    private int originalCardSortingOrder;

    private const float cardCancelAnimationTime = 0.2f;
    private const Ease cardAnimationEase = Ease.OutBack;

    private const float CardAboutToBePlayedOffsetY = 1.5f;
    private const float CardAnimationTime = 0.4f;
    [SerializeField] private BoxCollider2D cardArea;

    private bool isCardAboutToBePlayed;

    private bool isEnemyToBeSelected;
    private bool isCardToBeSelected;

    private List<GameObject> selectedObjects = new List<GameObject>();

    private void Update()
    {
        if (cardDisplayManager.isMoving())
        {
            //Debug.Log("cardDisplayManager.isMoving");
            return;
        }

        if (isCardAboutToBePlayed)
        {
            //Debug.Log("isCardAboutToBePlayed");
            if (Input.GetMouseButtonDown(0))
            {
                if(isCardToBeSelected)
                {
                    DetectHandCardSelection();
                }
                else if(isEnemyToBeSelected)
                {
                    DetectEnemySelection();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                selectedObjects.Clear();
                isCardAboutToBePlayed = false;
                isCardToBeSelected = false;
                isEnemyToBeSelected = false;
                DetectCardUnselection();
            }
            else
            {
                PlaySelectedCard();
            }

            
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("DetectCardSelection");
            DetectCardSelection();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DetectCardUnselection();
        }

        if (selectedCard != null)
        {
            //Debug.Log("UpdateSelectedCard");
            UpdateSelectedCard();
        }
    }

   

    private void DetectCardSelection()
    {
        if (selectedCard != null)
            return;

        Debug.Log("selectedCard == null");
        
        // 检查玩家是否在卡牌的上方作了点击操作
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward,
            Mathf.Infinity, cardLayer);

        if (hitInfo.collider != null)
        {
            Debug.Log("hitInfo.collider != null");
            var card = hitInfo.collider.GetComponent<CrackedCardObject>();
            var cardData = card.data;

            //TODO: judge if it's a complete card

            if (CardUtils.CardCanBePlayed(cardData, playerMana))
            {
                Debug.Log("CardCanBePlayed");
                selectedCard = hitInfo.collider.gameObject;
                originalCardPosition = selectedCard.transform.position;
                originalCardRotation = selectedCard.transform.rotation;
                originalCardSortingOrder = selectedCard.GetComponent<SortingGroup>().sortingOrder;
            }
        }
    }

    private void DetectHandCardSelection()
    {
        
        // 检查玩家是否在卡牌的上方作了点击操作
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward,
            Mathf.Infinity, cardLayer);

        if (hitInfo.collider != null)
        {
            var card = hitInfo.collider.GetComponent<CrackedCardObject>();
            if(card == null)
            {
                return;
            }

            selectedObjects.Add(hitInfo.collider.gameObject);
            isCardToBeSelected = false;
        }
    }

    private void DetectEnemySelection()
    {
        
        // 检查玩家是否在敌人的上方作了点击操作
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward,
            Mathf.Infinity, enemyLayer);

        if (hitInfo.collider != null)
        {
            var enemy = hitInfo.collider.GetComponent<CharacterObject>();
            if(enemy == null)
            {
                return;
            }

            selectedObjects.Add(hitInfo.collider.gameObject);
            isEnemyToBeSelected = false;
        }
    }
    
    private void DetectCardUnselection()
    {
        if (selectedCard != null)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                selectedCard.transform.DOMove(originalCardPosition, cardCancelAnimationTime).SetEase(cardAnimationEase);
                selectedCard.transform.DORotate(originalCardRotation.eulerAngles, cardCancelAnimationTime);
            });
            sequence.OnComplete(() =>
            {
                selectedCard.GetComponent<SortingGroup>().sortingOrder = originalCardSortingOrder;
                selectedCard = null;
            });
        }
    }
    
    private void UpdateSelectedCard()
    {
        // 处理鼠标左键已经松开时的逻辑
        if (Input.GetMouseButtonUp(0))
        {
            var card = selectedCard.GetComponent<CrackedCardObject>();
            
            // 如果选中卡牌后，鼠标左键已经松开且选中的卡牌是准备打出状态
            if (card.State == CrackedCardObject.CardState.AboutToBePlayed)
            {
                // 设置此状态用于屏蔽掉卡牌的拖拽效果
                isCardAboutToBePlayed = true;
                
                // 移动非攻击卡牌到效果施放区域
                var sequence = DOTween.Sequence();

                sequence.Append(selectedCard.transform.DOMove(cardArea.bounds.center, CardAnimationTime)
                    .SetEase(cardAnimationEase));
                sequence.AppendInterval(CardAnimationTime + 0.1f);
                sequence.AppendCallback(() =>
                {
                    // 开始施放效果
                    PlaySelectedCard();
                    //selectedCard = null;
                    //isCardAboutToBePlayed = false;
                });

                selectedCard.transform.DORotate(Vector3.zero, CardAnimationTime);
            }
            //  如果选中卡牌后，发现不想打这张牌，且卡牌移动的距离还不足以触发让非攻击卡牌进入
            //  效果施放区的动画，这时重置卡牌状态，并把它放置会起始位置
            else
            {
                //TODO: Add sth to instruct
                var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var hitInfo = Physics2D.RaycastAll(mousePosition, Vector3.forward,
                    Mathf.Infinity, cardLayer);

                if (hitInfo.Length == 2)
                {
                    var card_1 = hitInfo[0].collider.GetComponent<CrackedCardObject>();
                    var card_2 = hitInfo[1].collider.GetComponent<CrackedCardObject>();
                    List<CrackedCardData> cards = new List<CrackedCardData>();
                    cards.Add(card_1.data);
                    cards.Add(card_2.data);
                    var combined_card = CardUtils.Combine(cards);
                    if (combined_card != null)
                    {
                        cards.Clear();
                        cards.Add(combined_card);
                        cardDisplayManager.DistroyCardInHand(card_1.gameObject);
                        cardDisplayManager.DistroyCardInHand(card_2.gameObject);
                        cardDisplayManager.CreateHandCards(cards);
                    }
                    else
                    {
                        card.SetState(CrackedCardObject.CardState.InHand);
                        selectedCard.GetComponent<CrackedCardObject>().Reset(() => selectedCard = null);
                    }
                }

                else
                {
                    card.SetState(CrackedCardObject.CardState.InHand);
                    selectedCard.GetComponent<CrackedCardObject>().Reset(() => selectedCard = null);
                }
            }
        }
        
        if (selectedCard != null)
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedCard.transform.position = mousePosition;

            var card = selectedCard.GetComponent<CrackedCardObject>();
            
            // 检测非攻击卡牌在选中后的距离是否足够大，可以改变它的出牌状态
            // 如果足够大，则脱离手握牌状态，进入待出牌状态
            if (mousePosition.y > originalCardPosition.y + CardAboutToBePlayedOffsetY)
            {
                card.SetState(CrackedCardObject.CardState.AboutToBePlayed);
            }
            else
            {
                card.SetState(CrackedCardObject.CardState.InHand);
            }
        }
    }
    
    // 重载基类的PlaySelectedCard函数，用于解析非攻击效果类卡牌
    protected override void PlaySelectedCard()
    {
        //TODO: add target selection

        var card = selectedCard.GetComponent<CrackedCardObject>().data;
        int target_count = 0;
        var left_piece = card.card_pieces[2] as EffectPieceData;
        var right_piece = card.card_pieces[3] as EffectPieceData;
        if (left_piece != null)
        {
            for(int i = 0; i < left_piece.events.Count; i++)
            {
                var card_event = left_piece.events[i].card_event;
                if(card_event.primary_target == EventTargetType.SelectedEnemy)
                {
                    target_count++;
                    if(target_count > selectedObjects.Count)
                    {
                        Debug.Log("target_count > selectedObjects.Count");
                        isEnemyToBeSelected = true;
                        return;
                    }
                    Debug.Log("target_count <= selectedObjects.Count");
                }
                else if(card_event.primary_target == EventTargetType.SelectedHandCard)
                {
                    target_count++;
                    if(target_count > selectedObjects.Count)
                    {
                        isCardToBeSelected = true;
                        return;
                    }
                }
            }
        }

        if (right_piece != null)
        {
            for(int i = 0; i < right_piece.events.Count; i++)
            {
                var card_event = right_piece.events[i].card_event;
                if(card_event.primary_target == EventTargetType.SelectedEnemy)
                {
                    target_count++;
                    if(target_count > selectedObjects.Count)
                    {
                        isEnemyToBeSelected = true;
                        return;
                    }
                }
                else if(card_event.primary_target == EventTargetType.SelectedHandCard)
                {
                    target_count++;
                    if(target_count > selectedObjects.Count)
                    {
                        isCardToBeSelected = true;
                        return;
                    }
                }
            }
        }

        base.PlaySelectedCard();

        isCardAboutToBePlayed = false;
        selectedCard = null;
        
        effectResolutionManager.ResolveCardEffects(card, selectedObjects);
        selectedObjects.Clear();

    }

    public bool HasSelectedCard()
    {
        return selectedCard != null;
    }
}

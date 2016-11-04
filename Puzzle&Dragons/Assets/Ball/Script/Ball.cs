using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour{
    
    public Vector3 _originPos = Vector3.zero;
    public BallList _ballList = null;
    public int _ballId = 0;
    
    private int orderInLayerNum_ = 0;
    
    void Start()
    {
        _ballId = this.transform.GetSiblingIndex();
        _originPos = this.transform.position;
        orderInLayerNum_ = this.transform.GetComponent<SpriteRenderer>().sortingOrder;
    }
    
    public void OnMouseDrag()
	{
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(newPos.y > 1){
            newPos.y = 1;
        }
        this.transform.position = new Vector3(newPos.x, newPos.y, 0);
        this.transform.GetComponent<SpriteRenderer>().sortingOrder = orderInLayerNum_ + 1;
    }
    
    public void OnMouseUp()
    {
        this.transform.position = _ballList._ballPosList[_ballId];
        this.transform.GetComponent<SpriteRenderer>().sortingOrder = orderInLayerNum_;
        CheckLine();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        int id = other.transform.GetComponent<BallCollider>()._ballId;
        for(int i = 0; i < _ballList._allBallList.Length; ++i){
            if(_ballList._allBallList[i]._ballId == id){
                _ballList._allBallList[i]._ballId = this._ballId;
                _ballList._allBallList[i].ResetPosition();
                this._ballId = id;
                return;
            }
        }
    }

    public void ResetPosition()
    {
        this.transform.position = _ballList._ballPosList[_ballId];
        this.transform.GetComponent<SpriteRenderer>().sortingOrder = orderInLayerNum_;
    }

    public void Fade() 
    {
        //Color color = this.transform.GetComponent<SpriteRenderer>().color;
        //this.transform.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
        MoveUp();
        _ballList.CheckAllBallFall();
    }

    public void CheckFall() {
        RaycastHit2D[] ballDownList = Physics2D.LinecastAll(this.transform.position, this.transform.position - transform.up * 1, 1);
        if (ballDownList.Length == 1 && _ballId < 15) {
            Debug.Log("a:"+transform.name);
            _ballId += 5;
            if (_ballId >= 0) {
                ResetPosition();
            }
        }
    }

    private void CheckLine()
    {
        int horizontalBallNum = 0;
        int verticalBallNum = 0;
        RaycastHit2D[] ballLeftList = Physics2D.LinecastAll(this.transform.position, this.transform.position - transform.right * 10, 1);
        RaycastHit2D[] ballRightList = Physics2D.LinecastAll(this.transform.position, this.transform.position + transform.right * 10, 1);
        RaycastHit2D[] ballUpList = Physics2D.LinecastAll(this.transform.position, this.transform.position + transform.up * 10, 1);
        RaycastHit2D[] ballDownList = Physics2D.LinecastAll(this.transform.position, this.transform.position - transform.up * 10, 1);

        //ballLeftList[0] is this.transform
        for (int i = 1; i < ballLeftList.Length; ++i) {
            Sprite sprite = ballLeftList[i].transform.GetComponent<SpriteRenderer>().sprite;
            if (this.transform.GetComponent<SpriteRenderer>().sprite != sprite) {
                break;
            }
            ++horizontalBallNum;
        }

        for (int i = 1; i < ballRightList.Length; ++i) {
            Sprite sprite = ballRightList[i].transform.GetComponent<SpriteRenderer>().sprite;
            if (this.transform.GetComponent<SpriteRenderer>().sprite != sprite) {
                break;
            }
            ++horizontalBallNum;
        }

        for (int i = 1; i < ballUpList.Length; ++i) {
            Sprite sprite = ballUpList[i].transform.GetComponent<SpriteRenderer>().sprite;
            if (this.transform.GetComponent<SpriteRenderer>().sprite != sprite) {
                break;
            }
            ++verticalBallNum;
        }

        for (int i = 1; i < ballDownList.Length; ++i) {
            Sprite sprite = ballDownList[i].transform.GetComponent<SpriteRenderer>().sprite;
            if (this.transform.GetComponent<SpriteRenderer>().sprite != sprite) {
                break;
            }
            ++verticalBallNum;
        }

        //add this.transform
        ++horizontalBallNum;
        ++verticalBallNum;

        if (horizontalBallNum >= 3) {
            for (int i = 1; i < ballRightList.Length; ++i) {
                Sprite sprite = ballRightList[i].transform.GetComponent<SpriteRenderer>().sprite;
                if (this.transform.GetComponent<SpriteRenderer>().sprite == sprite) {
                    ballRightList[i].transform.GetComponent<Ball>().Fade();
                } else {
                    break;
                }
            }

            for (int i = 1; i < ballLeftList.Length; ++i) {
                Sprite sprite = ballLeftList[i].transform.GetComponent<SpriteRenderer>().sprite;
                if (this.transform.GetComponent<SpriteRenderer>().sprite == sprite) {
                    ballLeftList[i].transform.GetComponent<Ball>().Fade();
                } else {
                    break;
                }
            }
        }

        if (verticalBallNum >= 3) {
            for (int i = 1; i < ballUpList.Length; ++i) {
                Sprite sprite = ballUpList[i].transform.GetComponent<SpriteRenderer>().sprite;
                if (this.transform.GetComponent<SpriteRenderer>().sprite == sprite) {
                    ballUpList[i].transform.GetComponent<Ball>().Fade();
                } else {
                    break;
                }
            }

            for (int i = 1; i < ballDownList.Length; ++i) {
                Sprite sprite = ballDownList[i].transform.GetComponent<SpriteRenderer>().sprite;
                if (this.transform.GetComponent<SpriteRenderer>().sprite == sprite) {
                    ballDownList[i].transform.GetComponent<Ball>().Fade();
                } else {
                    break;
                }
            }
        }

        if (horizontalBallNum >= 3 || verticalBallNum >= 3) {
            Fade();
        }

    }

    private void MoveUp() 
    {
        this.transform.position += transform.up * 10;
        Debug.Log("_ballId  " + _ballId + transform.name);
        _ballId -= 20;
        this.transform.GetComponent<SpriteRenderer>().sprite = _ballList._ballSpriteList[Random.Range(0, 4)];
    }
	
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BallList : MonoBehaviour {

	public bool _change = false;
    public Ball[] _allBallList = null;
    public Sprite[] _ballSpriteList = null;
    public Vector3[] _ballPosList = null;

    private bool fall_ = false;
    private int lineNum_ = 0;

    void Start() 
    {
        _allBallList = new Ball[this.transform.childCount];
        _ballPosList = new Vector3[this.transform.childCount];
        for(int i = 0;i < this.transform.childCount; ++i){
            _allBallList[i] = this.transform.GetChild(i).GetComponent<Ball>();
            _ballPosList[i] = this.transform.GetChild(i).position;
            this.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = _ballSpriteList[Random.Range(0, 4)];
        }
        
    }
    
	void Update () 
    {
        if(_change){
            _change = false;
            SceneManager.LoadScene("Ball");
        }
        if (fall_) {
            StartCoroutine(CheckAllBall());
        }
    }

    public void CheckAllBallFall() {
        fall_ = true;
    }

    IEnumerator CheckAllBall()
    {
        for (int i = _allBallList.Length - 1; i >= 0; --i) {
            _allBallList[i].CheckFall();
        }
        ++lineNum_;
        if (lineNum_ == 20) {
            fall_ = false;
            lineNum_ = 0;
        }
        yield return new WaitForSeconds(0.1f);
    }
    
}

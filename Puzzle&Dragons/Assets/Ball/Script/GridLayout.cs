using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GridLayout : MonoBehaviour {
    
    public Transform _targetList = null;
    public int _horizontalCellNum = 0;
    public float _horizontalSpacingNum = 0;
    public float _verticalSpacingNum = 0;
    public bool _isUpdate = false;
    
    private int verticalCellNum_ = 0;
    private float horizontalLength_ = 0;
    private int centerIndex_ = 0;
	
	void Update () {
	   if(_isUpdate){
           _isUpdate = false;
           if(_targetList == null){
               Debug.LogError("_targetList is null!");
           }
           if(_targetList.childCount == 0){
               Debug.LogError("Cell num is 0!");
           }else{
               if(_targetList.childCount % _horizontalCellNum == 0){
                   verticalCellNum_ = (int)_targetList.childCount / _horizontalCellNum;
               }else{
                   verticalCellNum_ = (int)_targetList.childCount / _horizontalCellNum + 1;
               }

               horizontalLength_ = _horizontalSpacingNum * (_horizontalCellNum - 1);
               _targetList.GetChild(0).position = new Vector3(-horizontalLength_/2, 0, 0);
               float posX = _targetList.GetChild(0).position.x;
               float posY = _targetList.GetChild(0).position.y;

               for(int i = 1; i < _targetList.childCount; ++i){
                   if(i < _horizontalCellNum){
                       posX = posX + _horizontalSpacingNum;
                       _targetList.GetChild(i).position = new Vector3(posX, 0, 0);
                   }else{
                       int num = i / _horizontalCellNum;
                       posX = _targetList.GetChild(i - _horizontalCellNum * num).position.x;
                       posY = - num * _verticalSpacingNum;
                       _targetList.GetChild(i).position = new Vector3(posX, posY, 0);
                   }
               }

           }
       }
	}
    
}

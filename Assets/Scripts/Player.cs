using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//角色移动以及检测
public class Player : MonoBehaviour
{
    //定义一个Map类
    private MapCreator BoxMap;
    //场景的总数
    private int allSceneCount;
    //行走的步数
    public int walkCount = 0;
    //是否胜利
    private bool isWin = false;
   
    void Start()
    {
        //加载场景的总数
        allSceneCount = SceneManager.sceneCountInBuildSettings;
        //获得类型为 MapCreator 的已加载的激活对象
        BoxMap = FindObjectOfType<MapCreator>();
    }
    void Update()
    {
        Move();
        if ( isWin )
        {
            //显示游戏胜利的图片
            SceneContral.Instance.WinImage.gameObject.SetActive( true );
        }
        isWin = false;
    }
  
    /// <summary>
    /// 玩家移动，并检测
    /// </summary>
    void Move()
    {
        int moveX = 0;  
        int moveZ = 0;
        if ( Input.GetKeyDown( KeyCode.RightArrow ) )
        {
            moveX++;
        }
        else if ( Input.GetKeyDown(KeyCode.LeftArrow ))
        {
            moveX--;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveZ++;
        }
        else if ( Input.GetKeyDown( KeyCode.DownArrow ) )
        {
            moveZ--;
        }
        //玩家的下个位置
        int nextX = moveX + ( int ) transform.position.x;
        int nextZ = moveZ + ( int ) transform.position.z;
        //判断下位置是不是墙
        if ( IsWall( nextX , nextZ ) )  return;
        //判断下个位置是不是箱子
        if ( IsBox(nextX,nextZ) )
        {
            //得到下下个位置
            int nextNextX = nextX + moveX;
            int nextNextZ = nextZ + moveZ;
            //判断下个位置是不是墙或则盒子
            if ( IsBox( nextNextX , nextNextZ ) || IsWall( nextNextX , nextNextZ ) ) return;
            GameObject box = GetBox(nextX,nextZ);
            box.transform.position = new Vector3( nextNextX ,0.5f, nextNextZ );
            //更新结构
            BoxMap.GetPosBoxMap().Remove( BoxMap.TwoDToOneD( nextX , nextZ ) );
            BoxMap.GetPosBoxMap().Add( BoxMap.TwoDToOneD( nextNextX , nextNextZ ) , box );
        }
        if ( isMove( moveX , moveZ ) )
        {
            //玩家移动到下一个位置
            transform.position = new Vector3( nextX , 0.5f , nextZ );
            SceneContral.Instance.walkCount++;
        }
        CheckWin(); 
    }
    /// <summary>
    /// 根据x,z,坐标判断是否是墙
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    bool IsWall( int x , int z )
    {
        return BoxMap.GetWallPosSet().Contains( BoxMap.TwoDToOneD( x , z ) );
    }
    /// <summary>
    ///  根据x,z,坐标判断是否是盒子
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    bool IsBox( int x , int z )
    {
        return BoxMap.GetPosBoxMap().ContainsKey( BoxMap.TwoDToOneD( x , z ) );
    }
    /// <summary>
    /// 根据x,y位置得到盒子的GameObject
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    GameObject GetBox( int x , int z )
    {
        return BoxMap.GetPosBoxMap()[BoxMap.TwoDToOneD( x , z )];
    }
    /// <summary>
    /// 判断玩家是否移动
    /// </summary>
    /// <param name="x"> 水平偏移量</param>
    /// <param name="z"> 锤子偏移量</param>
    /// <returns></returns>
    bool isMove( int x , int z )
    {
        if ( x == 0 && z == 0 )
        {
            return false;
        }
        else return true;
    }
    /// <summary>
    /// 检测游戏是否胜利
    /// </summary>
    void CheckWin()
    {
        int num = 0;
        //遍历目标点的HashSet
        foreach ( var tar_pos in BoxMap.GetTargetPosList() )
        {
            //如果目标点的位置与盒子的位置重合
            if ( BoxMap.GetPosBoxMap().ContainsKey( tar_pos ) )  ++num;
        }
        if ( num == BoxMap.GetTargetPosList().Count )
        {
            //加载下一个场景 
            /*
             * 1.获取当前场景
             * 2.获取当前场景的索引
             * 3.如果存在下一个场景，则加载场景
             */
            Scene scene = SceneManager.GetActiveScene();
            int sceneCount  = scene.buildIndex;
            if ( sceneCount == allSceneCount - 1 )
            {
                //游戏胜利
                isWin = true;
            }
            if ( sceneCount+1 <allSceneCount )
            {
                SceneManager.LoadScene( sceneCount + 1 );
            }
        }
    }
}

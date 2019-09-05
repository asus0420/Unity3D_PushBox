using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//关卡管理
public class SceneContral : MonoBehaviour
{
    #region 关卡设置
    //关卡标题
    public Text sceneLevel;
    //关卡编号
    private int levelNumber;
    private Scene currentScene; // 当前的Scene
    //关卡总数
    private int allScenesNumber;
    #endregion

    #region 按钮
    //上一关与下一关Button引用
    public Button Btn_Next;
    public Button Btn_Last;
    #endregion

    #region 组件UI
    //对话框
    public Image tipWindow;
    //步数检测的Text组件
    public Text walkCountText;
    //行走的步数
    public int walkCount = 0;
    //游戏胜利
    public Image WinImage;
    #endregion

    /// <summary>
    /// SceneContral单实例
    /// </summary>
    private static SceneContral instance;
    public static SceneContral Instance
    {
        get
        {
            return SceneContral.instance;
        }
    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(WinImage!=null)
        {
            //游戏胜利图片不可见
            WinImage.gameObject.SetActive( false );
        }
        //获得当前Scene
        currentScene = SceneManager.GetActiveScene();
        //获得当前Scene的编号
        levelNumber = currentScene.buildIndex;
        //Scene总数
        allScenesNumber = SceneManager.sceneCountInBuildSettings;
        //设置为不可见
        tipWindow.gameObject.SetActive( false );
          //获取当前关卡编号
        sceneLevel.text = "第"+levelNumber+"关";
    }
    void Update()
    {
        walkCountText.text = "步数\n" + walkCount.ToString();
    }
    /// <summary>
    /// 点击加载主菜单
    /// </summary>
    public void ClickMenu()
    {
        //加载主菜单
        SceneManager.LoadScene( 0 );
    }
   /// <summary>
   /// 跳到上一关
   /// </summary>
    public void ClickLastScene()
    {
        if ( levelNumber == 1 )
        {
            //获取对话框的内容Text组件
            Text txtContent = tipWindow.transform.GetChild( 0 ).GetComponent<Text>();
            txtContent.text = "已经是第一关了！";
            tipWindow.gameObject.SetActive( true );
        }
        else
        {
            //关卡数--
            levelNumber--;
            SceneManager.LoadScene( levelNumber );
        }
    }


    public void ClickNextScene()
    {
        if ( levelNumber +1>= allScenesNumber)
        {
            //获取对话框的内容Text组件
            Text txtContent = tipWindow.transform.GetChild( 0 ).GetComponent<Text>();
            txtContent.text = "已经是第最后一关了！";
            tipWindow.gameObject.SetActive( true );
        }
        else
        {
            //关卡数++
            levelNumber++;
            SceneManager.LoadScene( levelNumber );
        }
 
    }
    /// <summary>
    /// 重置当前关卡
    /// </summary>
    public void ClickRestart()
    {
        //重置当前游戏
        SceneManager.LoadScene( levelNumber );
    }
    /// <summary>
    /// 取消按钮，使得tipWindow不可见
    /// </summary>
    public void ClickCancel()
    {
        if ( tipWindow.enabled )
        {
            tipWindow.gameObject.SetActive( false );
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//开始界面
public class StartScene : MonoBehaviour
{
    //获取Logo
    private Text txtLogo;
    //Logo的字体大小
    private int logoSize;
    //获取tipWindow
    public Image tipWindow;
    void Start()
    {
        //获取Logo上的Text组件
        txtLogo = transform.Find( "Logo" ).gameObject.GetComponent<Text>();
        //获得字体大小
        logoSize = txtLogo.fontSize;
        //开始游戏时，使得tipWindow不可见
        tipWindow.gameObject.SetActive( false );
    }
    void Update()
    {
        EffectLogo();
    }
    /// <summary>
    /// 字体特效
    /// </summary>
    void EffectLogo()
    {
        //使得字体大小在220-250之间变化
        txtLogo.fontSize = (int)Mathf.PingPong( Time.time * 220 / 3 , 30 ) + 220;
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void ClickStart()
    {
        //加载第一个场景
        SceneManager.LoadScene( 1 );
    }
    /// <summary>
    /// 游戏简介
    /// </summary>
    public void ClickAbout()
    {
        //游戏简介
        //设置可见
        tipWindow.gameObject.SetActive( true );
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void ClickQuit()
    {
        //退出游戏
        Application.Quit();
    }
    /// <summary>
    /// 点击取消
    /// </summary>
    public void ClickCancel()
    {
        //设置不可见
        tipWindow.gameObject.SetActive( false );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建地图
public class MapCreator : MonoBehaviour
{
   //输入的地图
    public string[] map;
    //所有的预制体   0 Player,1 Box,2 Wall, 3 Target
    //public List<GameObject> allPrefabs  = new List<GameObject>();
    public GameObject[] allPrefabs;
    //墙和目标点位置
    private Dictionary<int,GameObject> pos_box_map;  //盒子位置
    private HashSet<int> wall_pos_set;                              //墙的位置
    private List<int> tar_pos_list;                                         //目标点位置
    //用2维转1维
    public const int MAPSIZE = 100;
    //建图的起始点
    public int left_top_x = -4;
    public int left_top_z = -4;
    public float left_top_y = 0.5f;

    private void Awake()
    {
        //初始化数据结构
        pos_box_map = new Dictionary<int , GameObject>();
        wall_pos_set = new HashSet<int>();
        tar_pos_list = new List<int>();
        //Application.Quit()  只会在平台上有用，在unity3d的模拟器中没用
    }

    void Start()
    {
        //从左到右，从上到下依次建图
        int row_pos = left_top_x;
        foreach ( var row in map )
        {
            int col_pos = left_top_z;
            for ( int i = 0 ; i < row.Length ; i++ )
            {
                Vector3 cell_pos = new Vector3( row_pos , 0.5f , col_pos );
                //实例化相应预制体，并储存在数据结构中
                if ( row[i] == '1' )  //墙
                {
                    Instantiate( allPrefabs[2] , cell_pos , Quaternion.identity );
                    wall_pos_set.Add( TwoDToOneD( row_pos , col_pos ) );
                }
                else if ( row[i] == '2' )//玩家
                {
                    Instantiate( allPrefabs[0] , cell_pos , Quaternion.identity );
                }
                else if ( row[i] == '3' )//盒子
                {
                    GameObject newBox = Instantiate( allPrefabs[1] , cell_pos , Quaternion.identity );
                    pos_box_map.Add( TwoDToOneD( row_pos , col_pos ) , newBox );
                }
                else if ( row[i] == '4' ) //目标点
                {
                    Instantiate( allPrefabs[3] , cell_pos , Quaternion.identity );
                    tar_pos_list.Add( TwoDToOneD( row_pos , col_pos ) );
                }
                col_pos++;
            }
            row_pos++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 返回储存箱子的数据结构
    /// </summary>
    /// <returns></returns>
    public Dictionary<int , GameObject> GetPosBoxMap()
    {
        return pos_box_map;
    }
    /// <summary>
    /// 返回储存墙的数据结构
    /// </summary>
    /// <returns></returns>
    public HashSet<int> GetWallPosSet()
    {
        return wall_pos_set;
    }
    /// <summary>
    /// 返回储存目标点的数据结构
    /// </summary>
    /// <returns></returns>
    public List<int> GetTargetPosList()
    {
        return tar_pos_list;
    }
    /// <summary>
    /// 将X,Z坐标转化成一个数，使得每一个坐标对应一个数，存储在数据结构中，便于通过坐标访问数据结构中的值
    /// </summary>
    /// <param name="x"> 坐标X</param>
    /// <param name="z"> 坐标Z</param>
    /// <returns></returns>
    public int TwoDToOneD( int x , int z )
    {
        return MAPSIZE * x + z;
    }
}

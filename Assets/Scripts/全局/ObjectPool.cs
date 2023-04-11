using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using static Bullet;
using static linshi;
using static MonsterBullet;
public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool    //对象池类
    {
        public string tag;          //对象池的Tag(名称)
        public GameObject prefab;   //对象池所保存的物体类型
        public int size;            //对象池的大小
    }
    public List<Pool> pools;

    Dictionary<string, Queue<GameObject>> poolDictionary;  //声明字典
    public static ObjectPool Instance;    //单例模式，便于访问对象池
    private void Awake()
    {
        Instance = this;
       

    }
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();     //为每个对象池创建队列
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);   //隐藏对象池中的对象
                objectPool.Enqueue(obj);//将对象入队
            }
            poolDictionary.Add(pool.tag, objectPool);   //添加到字典后可以通过tag来快速访问对象池
        }
    }

 
    public GameObject SpawnFromPool(string tag, Vector3 positon, Quaternion rotation)     //从对象池中获取对象的方法
    {
        if (!poolDictionary.ContainsKey(tag))  //如果对象池字典中不包含所需的对象池
        {
            Debug.Log("Pool: " + tag + " does not exist");
            return null;

        }
        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();  //出队，从对象池中获取所需的对象
        objectToSpawn.transform.position = positon;  //设置获取到的对象的位置
        objectToSpawn.transform.rotation = rotation; //设置对象的旋转
        objectToSpawn.SetActive(true);                //将对象从隐藏设为激活


        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        IPooledObjectMonster monsterpooledObj = objectToSpawn.GetComponent<IPooledObjectMonster>();
        if (pooledObj != null)  //判断，并不是所有对象都继承了该接口，例如Cube我想让它向上飞，Sphere则让它直接生成，Sphere就不必继承IPoolObject接口
        {
            pooledObj.OnObjectSpawn();  //调用重用时的方法
        }
        if (monsterpooledObj != null) 
        {
            monsterpooledObj.OnObjectSpawn();  //调用重用时的方法
        }
        poolDictionary[tag].Enqueue(objectToSpawn);     //再次入队，可以重复使用，如果需要的对象数量超过对象池内对象的数量，在考虑扩大对象池
        //这样重复使用就不必一直生成和消耗对象，节约了大量性能
        return objectToSpawn;  //返回对象
    }
}

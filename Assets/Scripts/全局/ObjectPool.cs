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
    public class Pool    //�������
    {
        public string tag;          //����ص�Tag(����)
        public GameObject prefab;   //��������������������
        public int size;            //����صĴ�С
    }
    public List<Pool> pools;

    Dictionary<string, Queue<GameObject>> poolDictionary;  //�����ֵ�
    public static ObjectPool Instance;    //����ģʽ�����ڷ��ʶ����
    private void Awake()
    {
        Instance = this;
       

    }
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();     //Ϊÿ������ش�������
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);   //���ض�����еĶ���
                objectPool.Enqueue(obj);//���������
            }
            poolDictionary.Add(pool.tag, objectPool);   //��ӵ��ֵ�����ͨ��tag�����ٷ��ʶ����
        }
    }

 
    public GameObject SpawnFromPool(string tag, Vector3 positon, Quaternion rotation)     //�Ӷ�����л�ȡ����ķ���
    {
        if (!poolDictionary.ContainsKey(tag))  //���������ֵ��в���������Ķ����
        {
            Debug.Log("Pool: " + tag + " does not exist");
            return null;

        }
        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();  //���ӣ��Ӷ�����л�ȡ����Ķ���
        objectToSpawn.transform.position = positon;  //���û�ȡ���Ķ����λ��
        objectToSpawn.transform.rotation = rotation; //���ö������ת
        objectToSpawn.SetActive(true);                //�������������Ϊ����


        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        IPooledObjectMonster monsterpooledObj = objectToSpawn.GetComponent<IPooledObjectMonster>();
        if (pooledObj != null)  //�жϣ����������ж��󶼼̳��˸ýӿڣ�����Cube�����������Ϸɣ�Sphere������ֱ�����ɣ�Sphere�Ͳ��ؼ̳�IPoolObject�ӿ�
        {
            pooledObj.OnObjectSpawn();  //��������ʱ�ķ���
        }
        if (monsterpooledObj != null) 
        {
            monsterpooledObj.OnObjectSpawn();  //��������ʱ�ķ���
        }
        poolDictionary[tag].Enqueue(objectToSpawn);     //�ٴ���ӣ������ظ�ʹ�ã������Ҫ�Ķ�����������������ڶ�����������ڿ�����������
        //�����ظ�ʹ�þͲ���һֱ���ɺ����Ķ��󣬽�Լ�˴�������
        return objectToSpawn;  //���ض���
    }
}

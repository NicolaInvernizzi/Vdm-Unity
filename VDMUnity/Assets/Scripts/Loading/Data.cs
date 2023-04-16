using UnityEngine;

public class Data : MonoBehaviour
{
    public SaveSystem saveSystem;
    public string id { get; private set; }
    public bool isActive { get; private set; }
    public Vector3 position { get; private set; }
    public Vector3 rotation { get; private set; }
    public void GetData()
    {
        id = gameObject.name;
        isActive = gameObject.activeSelf;
        position = gameObject.transform.position;
        rotation = gameObject.transform.eulerAngles;
    }
    public void RefreshData(ObjectData objectData)
    {
        gameObject.SetActive(objectData.isActive);
        gameObject.transform.position = FromArrayToVector3(objectData.position);
        Vector3 v3 = FromArrayToQuaternion(objectData.rotation);
        gameObject.transform.eulerAngles = v3;
        rotation = FromArrayToQuaternion(objectData.rotation);
    }
    public static Vector3 FromArrayToVector3(float[] array)
    {
        Vector3 vector3 = new Vector3();
        vector3.x = array[0];
        vector3.y = array[1];
        vector3.z = array[2];
        return vector3;
    }
    public static float[] FromVector3ToArray(Vector3 vector3)
    {
        float[] array = new float[3];
        array[0] = vector3.x;
        array[1] = vector3.y;
        array[2] = vector3.z;
        return array;
    }
    public static Vector3 FromArrayToQuaternion(float[] array)
    {
        Vector3 quaternion = new Vector3();
        quaternion.x = array[0];
        quaternion.y = array[1];
        quaternion.z = array[2];
        return quaternion;
    }
    public static float[] FromQuaternionToArray(Vector3 quaternion)
    {
        float[] array = new float[3];
        array[0] = quaternion.x;
        array[1] = quaternion.y;
        array[2] = quaternion.z;
        return array;
    }
    private void OnDestroy()
    {
        saveSystem.datas.Remove(this);       
    }
}
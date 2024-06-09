using UnityEngine;

public class RingStrategy : ActionStrategy
{
    public void Execute(object[] data)
    {
        if(data.Length > 0)
        {
            GameObject go = (GameObject)data[0];
            go.SetActive(false);
        }
    }
}

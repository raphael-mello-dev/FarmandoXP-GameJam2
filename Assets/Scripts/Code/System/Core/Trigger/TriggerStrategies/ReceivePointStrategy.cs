using UnityEngine;

public class ReceivePointStrategy: ActionStrategy
{
    public void Execute(object[] data)
    {
        if (data.Length > 0)
        {
            GameObject package = (GameObject)data[0];
            GameObject point = (GameObject)data[1];
            if (!package.activeInHierarchy)
            {
                return;
            }
            point.SetActive(false);
            package.SetActive(false);
        }
    }
}

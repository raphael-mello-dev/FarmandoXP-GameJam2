using UnityEngine;

public class CollectPointStrategy : ActionStrategy
{
    public void Execute(object[] data)
    {
        if (data.Length > 0)
        {
            GameObject package = (GameObject)data[0];
            if (package.activeInHierarchy)
            {
                return;
            }
            GameObject point = (GameObject)data[1];

            point.SetActive(false);
            package.SetActive(true);
        }
    }
}

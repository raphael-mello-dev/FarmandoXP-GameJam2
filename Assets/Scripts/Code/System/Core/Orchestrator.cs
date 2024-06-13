using UnityEngine;

public class Orchestrator : MonoBehaviour
{
    [SerializeField] private Delivery[] DayTasks;
    [SerializeField] private Delivery currentTaskView;

    private float points = 0f;

    private int currentTask = -1;
    public int CurrentTask { get => currentTask; set => currentTask = value; }
    public float Points { get => points; set => points = value; }

    public bool finishedDay = false;

    private void Start()
    {
        currentTask = -1;
        NextTask();
        finishedDay = false;
    }

    private void NextTask()
    {
        if(currentTask >= 0)
        {
            DayTasks[currentTask].status = MissionStatus.FINISHED;
            DayTasks[currentTask].collectPoint.OnTrigger -= OnCollectPoint;
            DayTasks[currentTask].receivePoint.OnTrigger -= OnReceivePoint;
            DayTasks[currentTask].package.SetActive(false);
            Points += DayTasks[currentTask].points;
        }
        currentTask++;

        if(currentTask >= DayTasks.Length)
        {
            finishedDay = true;
            Debug.Log("Finished day!");
            return;
        }
        currentTaskView = DayTasks[currentTask];
        DayTasks[currentTask].status = MissionStatus.STARTED;
        DayTasks[currentTask].collectPoint.OnTrigger += OnCollectPoint;
        DayTasks[currentTask].receivePoint.OnTrigger += OnReceivePoint;
        DayTasks[currentTask].package.SetActive(false);
    }

    public void OnCollectPoint(GameObject go, Collider other)
    {
        if(DayTasks[currentTask].status == MissionStatus.STARTED)
        {
            GameManager.Instance.AudioManager.PlaySFX(SFXs.Delivery);
            go.SetActive(false);
            DayTasks[currentTask].status = MissionStatus.COLLECTED;
            DayTasks[currentTask].package.SetActive(true);
        }
    }

    public void OnReceivePoint(GameObject go, Collider other)
    {
        if(DayTasks[currentTask].status == MissionStatus.COLLECTED)
        {
            GameManager.Instance.AudioManager.PlaySFX(SFXs.Delivery);
            go.SetActive(false);
            NextTask();
        }
    }

    public GameObject GetNextTarget()
    {
        if (finishedDay) return null;
        if (DayTasks[currentTask].status == MissionStatus.STARTED){
            return DayTasks[currentTask].collectPoint.gameObject;
        }

        if (DayTasks[currentTask].status == MissionStatus.COLLECTED){
            return DayTasks[currentTask].receivePoint.gameObject;
        }
        return null;
    }

    public bool WithPackage()
    {
        if(finishedDay) return false;
        if(currentTask >= 0)
        {
            return DayTasks[currentTask].status == MissionStatus.COLLECTED;
        }
        return false;
    }
}

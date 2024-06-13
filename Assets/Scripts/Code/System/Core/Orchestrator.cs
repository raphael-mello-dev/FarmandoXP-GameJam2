using UnityEngine;

public class Orchestrator : MonoBehaviour
{
    [SerializeField] private Delivery[] DayTasks;
    [SerializeField] private Delivery currentTaskView;
    [SerializeField] private MenuBinder menuBinder;

    private float points = 0f;
    [SerializeField] private float time = 0f;

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
        if(currentTask >= 0 && currentTask <= DayTasks.Length)
        {
            DayTasks[currentTask].status = MissionStatus.FINISHED;
            DayTasks[currentTask].collectPoint.OnTrigger -= OnCollectPoint;
            DayTasks[currentTask].receivePoint.OnTrigger -= OnReceivePoint;
            DayTasks[currentTask].package.SetActive(false);
            Points += (DayTasks[currentTask].history.points * CalculatePoints(DayTasks[currentTask], time));
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
        menuBinder.Delivery = DayTasks[currentTask];
    }

    public void OnCollectPoint(GameObject go, Collider other)
    {
        if(DayTasks[currentTask].status == MissionStatus.STARTED)
        {
            GameManager.Instance.AudioManager.PlaySFX(SFXs.Delivery);
            go.SetActive(false);
            DayTasks[currentTask].status = MissionStatus.COLLECTED;
            DayTasks[currentTask].package.SetActive(true);
            menuBinder.Delivery = DayTasks[currentTask];
        }
    }

    public void OnReceivePoint(GameObject go, Collider other)
    {
        if(DayTasks[currentTask].status == MissionStatus.COLLECTED)
        {
            menuBinder.Delivery = DayTasks[currentTask];
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

    public float CalculatePoints(Delivery delivery, float time)
    {
        float timeElapsed = this.time; // tempo da entrega
        float timeReference = delivery.history.baseTime;
        float points = 0f;

        switch (delivery.history.strategy)
        {
            case DeliveryStrategy.VELOCITY:
                points = Mathf.Clamp01(1 - (timeElapsed / timeReference));
                break;

            case DeliveryStrategy.INTIME:
                points = Mathf.Clamp01(1 - (Mathf.Abs(timeElapsed - timeReference) / timeReference));
                break;

            case DeliveryStrategy.TEMPERATURE:
                PlayerData playerData = FindAnyObjectByType<PlayerData>();
                int temperature = playerData.GetTemperaturePercent();
                int temperatureDelivery = delivery.history.temperatureReference;
                points = Mathf.Clamp01(1 - (Mathf.Abs(temperature - temperatureDelivery) / temperatureDelivery));
                break;
        }
        return points;
    }

    private void Update()
    {
        menuBinder.TimeDelivery = time;
        if(currentTaskView == null)
        {
            time = 0f;
            return;
        }

        if(currentTaskView.status == MissionStatus.COLLECTED)
        {
            time += Time.deltaTime;
            return;
        }

        time = 0f;
    }
}

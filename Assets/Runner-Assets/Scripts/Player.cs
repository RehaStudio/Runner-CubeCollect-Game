using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player :  PlayerController
{
    List<IStackObject> stackObjects = new List<IStackObject>();
    [Header("Player Settings")]
    [SerializeField] Animator animatorCharachter;
    [SerializeField] Transform stackSpawnPoint;
    [SerializeField] GameObject powerTrailEffect;
    [SerializeField] float powerUpSpeed = 10f;


    float normalSpeed;
    float timePowerUpMax = 5f;
    [SerializeField] int startCountOnStart = 4;
    float targetPlayerPosY;

    private void OnEnable()
    {
        EventManager.powerUp += EventManagerPowerUp;
        normalSpeed = currentSpeed;
        targetPlayerPosY = transform.position.y;
    }
    private void OnDisable()
    {
        EventManager.powerUp -= EventManagerPowerUp;
    }
    private void Start()
    {
        CreateStartObjects();
        base.Start();
    }
    public void CreateStartObjects()
    {
        for (int i = 0; i < startCountOnStart; i++)
        {
            AddNewStack(PrefabManager.Instance.CubeObjectPrefab.InstantiniatePool().GetComponent<IStackObject>(),0f);
        }
    }
    private void EventManagerPowerUp()
    {
        currentSpeed = powerUpSpeed;
        powerTrailEffect.SetActive(true);
        StartCoroutine(ActivatePowerUp());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsMoving)
            return;

        IStackObject _stack; 
        IObstacleObject _obstacle;

        if (other.TryGetComponent<IStackObject>(out _stack))
        {
            AddNewStack(_stack);
            return;
        }
        if (other.TryGetComponent<IObstacleObject>(out _obstacle))
        {
            if (_obstacle.AlreadyCollided)
                return;
            RemoveStack(_obstacle.Power);
            _obstacle.Collide();
            CreateDestroyParticle(other.transform.position);
            return;
        }
        if (other.GetComponent<FinishObject>())
        {
            WinAction();
            EventManager.LevelCompleted();
            return;
        }
    }
    void AddNewStack(IStackObject _stack,float smoothTime = .2f)
    {
        if (_stack.AlreadyCollided)
            return;
        if (IsMoving)
        {
            CreateCollectParticle(-stackObjects.Count * .5f + .2f);
            EventManager.CubeCountChanged.Invoke(1);
        }
        stackObjects.Add(_stack);
        _stack.SetPosition(stackObjects.Count,stackSpawnPoint, smoothTime);
        targetPlayerPosY = targetPlayerPosY +  _stack.Scale;
        transform.DOKill();
        transform.DOMoveY(targetPlayerPosY, smoothTime);
    
    }
    void RemoveStack()
    {
        if (stackObjects.Count == 0)
            return;
        IStackObject _stack = stackObjects[stackObjects.Count - 1];
        targetPlayerPosY = targetPlayerPosY - _stack.Scale;
        transform.DOKill();
        transform.DOMoveY(targetPlayerPosY, .2f);
        stackObjects.RemoveAt(stackObjects.Count - 1);
        EventManager.CubeCountChanged.Invoke(-1);
        _stack.Destroy();

        LevelLostControl();
    }
    void RemoveStack(int count)
    {
        for (int i = 0; i < count; i++)
        {
            RemoveStack();
        }
    }
    void CreateDestroyParticle(Vector3 position)
    {
        PrefabManager.Instance.DestroyParticle.InstantiniatePool().transform.position = position;
    }
    void CreateCollectParticle(float _y)
    {
        GameObject _collectParticle = PrefabManager.Instance.CollectParticle.InstantiniatePool();
        
        _collectParticle.transform.parent = transform;
        _collectParticle.transform.localPosition = new Vector3(0, _y, 0);
    }

    void LevelLostControl()
    {
        if (stackObjects.Count == 0)
        {
            StopMove();
            EventManager.LevelFailed();
        }
    }
    IEnumerator ActivatePowerUp()
    {
        float timePast = 0f;
        while (timePast <= timePowerUpMax)
        {
            timePast += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        DisablePowerUp();
    }
    public void DisablePowerUp()
    {
        currentSpeed = normalSpeed;
        EventManager.PowerUpEnded();
        powerTrailEffect.SetActive(false);
    }
    public void WinAction()
    {
        Prefs.AddPoint(stackObjects.Count* 10 / 4);
        StopMove();
        animatorCharachter.SetBool("win", true);
    }
}

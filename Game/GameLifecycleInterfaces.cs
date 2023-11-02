public interface IUpdatePerTime
{
    void UpdatePerTime(float time);
}

public interface IOfflineUpdate
{
    void UpdateWhileOffline(double time);
}

//public interface IOnGameStart
//{
//    void OnGameStart();
//}

public interface IOnGameEnd
{
    void OnGameEnd();
}
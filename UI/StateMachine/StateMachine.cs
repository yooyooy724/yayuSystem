using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using yayu.Event;

namespace yayu.UI.StateMachine
{
    public class StateMachine : IDisposable
    {
        public IEventSubscription<IState> OnChangeState => onChangeState;
        private CustomEvent<IState> onChangeState = new CustomEvent<IState>();

        private List<IState> _rootStates = new();
        private List<IState> currentState = new(); // 現在のステートのリスト

        IState _initialState;

        public void RegisterRootStateAsInitial(IState initialState)
        {
            if (_initialState != null)
            {
                YDebugger.LogError("Initial State is already set");
                return;
            }
            _initialState = initialState;
            RegisterRootState(initialState);
            ChangeState(initialState.path);
        }

        public void RegisterRootState(IState rootState)
        {
            _rootStates.Add(rootState);
            rootState.path = rootState.id; // ルートステートのパスをIDで初期化
            YDebugger.Log(rootState.path);
            InitializeStatePaths(rootState, rootState.id); // 子ステートのパスを初期化
        }

        private void InitializeStatePaths(IState state, string basePath)
        {
            foreach (var child in state.GetChildren())
            {
                child.path = basePath + "_" + child.id;
                InitializeStatePaths(child, child.path);
            }
        }

        public void ChangeState(string path)
        {
            var newState = FindState(path);
            if (newState == null)
            {
                YDebugger.LogError($"Don't Found State (path: {path})");
            }

            var newPathList = newState.path.Split('_').ToList();

            // 修正された共通パス長の計算
            int commonPathLength = 0;
            for (; commonPathLength < Math.Min(currentState.Count, newPathList.Count); commonPathLength++)
            {
                if (currentState[commonPathLength].id != newPathList[commonPathLength])
                    break;
            }

            // 不要なステートのExitを呼ぶ
            for (int i = currentState.Count - 1; i >= commonPathLength; i--)
            {
                currentState[i].Exit();
                //YDebugger.Log("exit", currentState[i].id);
                currentState.RemoveAt(i);
            }

            // 新しいステートのEnterを呼ぶ
            for (int i = commonPathLength; i < newPathList.Count; i++)
            {
                var state = FindState(string.Join("_", newPathList.Take(i + 1)));
                if (state == null) YDebugger.LogError($"Unexpected Error : Don't Found State (path: {path})");
                state.Enter();
                currentState.Add(state);
                //YDebugger.Log("enter", state.id);
            }

            onChangeState.Invoke(newState);
        }

        private IState FindState(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var statePath = path.Split('_').ToList();

            IState result = null;
            IEnumerable<IState> states = _rootStates;

            // 階層構造を辿りながらステートを探す
            foreach (var part in statePath)
            {
                result = null;
                foreach (var state in states)
                {
                    if (state.id == part)
                    {
                        result = state;
                        break; // 対応するIDが見つかったのでループを終了
                    }
                }

                if (result == null)
                {
                    return null; // ステートが見つからない場合は処理を終了
                }

                states = result.GetChildren(); // 次の階層のステートを探索
            }

            return result;
        }

        public string GetCurrentStatePath()
        {
            return currentState.LastOrDefault()?.path ?? string.Empty;
        }

        public IState GetState(string path)
        {
            return FindState(path);
        }

        public void Dispose()
        {
            _rootStates.Clear();
            _initialState = null;
            currentState.Clear();
        } 
    }

    public static class StateExtensions
    {
        public static IEnumerable<IState> GetAllStates(this IState state)
        {
            yield return state;
            foreach (var child in state.GetChildren().SelectMany(child => GetAllStates(child)))
            {
                yield return child;
            }
        }
    }
}

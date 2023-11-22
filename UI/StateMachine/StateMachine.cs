using System.Collections.Generic;
using System.Linq;

namespace yayu.StateMachine
{
    public class StateMachine
    {
        private List<IState> _rootStates = new();
        private List<IState> currentState = new(); // 現在のステートのリスト

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
            if (newState == null) return;

            var newPathList = newState.path.Split('_').ToList();
            var commonPathLength = currentState.Zip(newPathList, (current, newPath) => current.id == newPath).Count();

            // 不要なステートのExitを呼ぶ
            for (int i = currentState.Count - 1; i >= commonPathLength; i--)
            {
                currentState[i].Exit();
                currentState.RemoveAt(i);
            }

            // 新しいステートのEnterを呼ぶ
            for (int i = commonPathLength; i < newPathList.Count; i++)
            {
                var state = FindState(string.Join("_", newPathList.Take(i + 1)));
                state?.Enter();
                currentState.Add(state);
            }
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

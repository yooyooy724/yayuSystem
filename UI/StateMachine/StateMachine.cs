using System.Collections.Generic;
using System.Linq;

namespace yayu.StateMachine
{
    public class StateMachine
    {
        private List<IState> _rootStates = new();
        private List<IState> currentState = new(); // ���݂̃X�e�[�g�̃��X�g

        public void RegisterRootState(IState rootState)
        {
            _rootStates.Add(rootState);
            rootState.path = rootState.id; // ���[�g�X�e�[�g�̃p�X��ID�ŏ�����
            InitializeStatePaths(rootState, rootState.id); // �q�X�e�[�g�̃p�X��������
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

            // �s�v�ȃX�e�[�g��Exit���Ă�
            for (int i = currentState.Count - 1; i >= commonPathLength; i--)
            {
                currentState[i].Exit();
                currentState.RemoveAt(i);
            }

            // �V�����X�e�[�g��Enter���Ă�
            for (int i = commonPathLength; i < newPathList.Count; i++)
            {
                var state = FindState(string.Join("_", newPathList.Take(i + 1)));
                state?.Enter();
                currentState.Add(state);
            }
        }

        private IState FindState(string path)
        {
            var statePath = path.Split('_').ToList();

            IState result = null;
            IEnumerable<IState> states = _rootStates;

            // �K�w�\����H��Ȃ���X�e�[�g��T��
            foreach (var part in statePath)
            {
                result = null;
                foreach (var state in states)
                {
                    if (state.id == part)
                    {
                        result = state;
                        break; // �Ή�����ID�����������̂Ń��[�v���I��
                    }
                }

                if (result == null)
                {
                    return null; // �X�e�[�g��������Ȃ��ꍇ�͏������I��
                }

                states = result.GetChildren(); // ���̊K�w�̃X�e�[�g��T��
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

using UnityEngine;
using UnityEngine.EventSystems;
using R3;
using UnityEngine.UI;

public class CustomLongPress : MonoBehaviour
{
    //[SerializeField] private Graphic raycastTarget; // ����RaycastTarget�ɑ΂���Q�Ƃ�Inspector����A�T�C�����܂�
    //[SerializeField] private float longPressTime = 2f; // �������Ƃ݂Ȃ�����

    //private void OnEnable()
    //{
    //    // �}�E�X�g���K�[�̏�����
    //    enterTrigger = raycastTarget.gameObject.AddComponent<ObservablePointerEnterTrigger>();
    //    downTrigger = raycastTarget.gameObject.AddComponent<ObservablePointerDownTrigger>();
    //    exitTrigger = raycastTarget.gameObject.AddComponent<ObservablePointerExitTrigger>();
    //    upTrigger = raycastTarget.gameObject.AddComponent<ObservablePointerUpTrigger>();

    //    // �}�E�X��raycastTarget�̏�ɂ���ԁA�������𔻒�

    //    // �}�E�X��raycastTarget�̏�ɓ������Ƃ����ϑ�
    //    enterTrigger.OnPointerEnterAsObservable()
    //        // �}�E�X��raycastTarget�̏�ɓ�������A�}�E�X�������ꂽ�Ƃ����ϑ�
    //        .SelectMany(_ => downTrigger.OnPointerDownAsObservable())
    //        // �}�E�X�������ꂽ��Ԃ𖈃t���[���ϑ�
    //        .SelectMany(_ => Observable.EveryUpdate())
    //        // �}�E�X��raycastTarget����o���Ƃ��A�܂��̓}�E�X�������ꂽ�Ƃ��Ɋϑ����~
    //        .TakeUntil(exitTrigger.OnPointerExitAsObservable())
    //        .TakeUntil(upTrigger.OnPointerUpAsObservable())
    //        // �o�ߎ��Ԃ��Z�o
    //        .Select(_ => Time.deltaTime)
    //        // ������Ă���̍��v���Ԃ��v�Z
    //        .Scan((pastTime, deltaTime) => pastTime + deltaTime)
    //        // ������Ă���̍��v���Ԃ��������̎��Ԃ𒴂����ꍇ�ɂ̂ݒʒm
    //        .Where(pastTime => pastTime >= longPressTime)
    //        // �����������o���ꂽ�Ƃ��Ƀ��O�o��
    //        .Subscribe(_ => Debug.Log("Long press detected"))
    //        // ����GameObject���j�����ꂽ�Ƃ��Ɏ����I�ɍw�ǉ�������悤�ݒ�
    //        .AddTo(this);
    //}
}

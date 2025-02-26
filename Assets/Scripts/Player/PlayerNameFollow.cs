using UnityEngine;
using TMPro;

public class PlayerNameFollow : MonoBehaviour
{
    // �C���X�y�N�^�[����v���C���[�I�u�W�F�N�g��ݒ�ł���悤�ɂ���
    [SerializeField] private GameObject playerObject;

    // �v���C���[���̕\���ʒu�I�t�Z�b�g
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, 0f);

    private void Start()
    {
        // �v���C���[�I�u�W�F�N�g���ݒ肳��Ă��Ȃ��ꍇ�̓G���[���O���o��
        if (playerObject == null)
        {
            Debug.LogError("�v���C���[�I�u�W�F�N�g���ݒ肳��Ă��܂���I");
            return;
        }
    }

    private void LateUpdate()
    {
        // �v���C���[�I�u�W�F�N�g���Ȃ��ꍇ�͏������Ȃ�
        if (playerObject == null)
            return;

        // �v���C���[�̈ʒu�ɍ��킹�Ė��O�̈ʒu���X�V
        // LateUpdate���g�p���邱�ƂŁA�v���C���[�̈ړ���ɖ��O�̈ʒu���X�V
        transform.position = playerObject.transform.position + offset;

        // �����J�����ɑ΂��ď�ɐ��ʂ������悤�ɂ������ꍇ�͈ȉ��̃R�[�h���g�p
        // transform.rotation = Camera.main.transform.rotation;
    }
}
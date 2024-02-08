using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDebufC : MonoBehaviour
{
    [Header("�f�o�t���ɌĂяo���G�t�F�N�g"),SerializeField]
    private ParticleSystem debufEffect;
    [Header("�W�����v�f�o�t�A�C�R��"),SerializeField]
    private Image jumpDebufIcon;
    [Header("�ړ��f�o�t�A�C�R��"),SerializeField]
    private Image moveDebufIcon;
    [SerializeField] private PlayerC playerC;

    //���Ԋ֌W
    private float currentMoveDebufTime = 0.0f;
    private int maxMoveDebufTime = 0;
    private float currentJumpDebufTime = 0.0f;
    private int maxJumpDebufTime = 0;

    //�{���֌W
    private float moveDebufMag = 1.0f;
    private float oldMoveDebufMag = 0.0f;
    private float jumpDebufMag = 1.0f;
    private float oldJumpDebufMag = 0.0f;

    //�t���O�֌W
    private bool onMoveDebuf = false;
    private bool onJumpDebuf = false;

    //PlayerManager
    private PlayerManager playerManager;

    public float MoveDebufMag
    {
        get { return this.moveDebufMag; }
    }
    public float JumpDebufMag
    {
        get { return this.jumpDebufMag;}
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        jumpDebufIcon.enabled = false;
        moveDebufIcon.enabled = false;
        moveDebufMag = playerManager.DefSpeedMag;
        jumpDebufMag = playerManager.DefJumpMag;
    }

    // Update is called once per frame
    void Update()
    {
        if(onMoveDebuf)
            CountMoveDebuf();
        if(onJumpDebuf)
            CountJumpDebuf();
    }

    #region//�ړ��f�o�t
    //�ړ��p�f�o�t�N��
    public void ActiveMoveDebuf(int time, float mag)
    {
        //�ŏ��i���߂ăf�o�t���󂯂����j�̏���
        if(moveDebufMag == playerManager.DefSpeedMag && !onMoveDebuf)
        {
            onMoveDebuf = true;
            //�v���C���[�����̃f�o�t���󂯂Ă��Ȃ���ԂȂ�
            if (!onJumpDebuf)
            {
                ParticleSystem newPar = Instantiate(debufEffect);
                newPar.transform.position = playerC.PopObject.transform.position;
                newPar.Play();
            }
            moveDebufIcon.enabled = true;
            //�����̌v�Z
            var def = 0.0f;
            if(moveDebufMag > mag) def = moveDebufMag - mag;
            else def = mag - moveDebufMag;

            moveDebufMag -= def;
            oldMoveDebufMag = moveDebufMag;

            maxMoveDebufTime = time;
        }
        //�f�o�t�����擾�����{���̕��������ꍇ
        else if (onMoveDebuf)
        { 
            //�{���̔�r
            var mag1 = oldMoveDebufMag;
            var mag2 = playerManager.DefSpeedMag - mag;
            //����̔{�����O��̔{���������Ă���i���l�I�ɂ͏��������j�Ȃ�
            if(mag1 > mag2)
            {
                moveDebufMag = mag2;
                //�t�^�����\��̌��ʎ��Ԃ��O��̕t�^���Ԃ������Ă���Ȃ�
                if (maxMoveDebufTime < time)
                {
                    //���s���Ԃ̃��Z�b�g
                    currentMoveDebufTime = 0.0f;
                    maxMoveDebufTime = time;
                }
            }
            //������Ă���Ȃ���ɉ������Ȃ�
            else moveDebufMag = mag1;
        }
    }
    //���Ԍv�Z�i�ړ��p�j
    private void CountMoveDebuf()
    {
        currentMoveDebufTime += 0.01f;
        if(currentMoveDebufTime >= maxMoveDebufTime)
            EndMoveDebuf();
    }
    //�ړ��f�o�t�̏I��
    private void EndMoveDebuf()
    {
        onMoveDebuf = false;
        moveDebufIcon.enabled = false;
        moveDebufMag = playerManager.DefSpeedMag;
        currentMoveDebufTime = 0.0f;
        oldMoveDebufMag = 0.0f;
        maxMoveDebufTime = 0;
        if (!onJumpDebuf && !onMoveDebuf)
            Destroy(this.debufEffect);
    }
    #endregion

    #region//�W�����v�p
    //�W�����v�p�f�o�t�N��
    public void ActiveJumpDebuf(int time, float mag)
    {
        //�ŏ��i���߂ăf�o�t���󂯂����j�̏���
        if (jumpDebufMag == playerManager.DefJumpMag && !onJumpDebuf)
        {
            onMoveDebuf = true;
            //�v���C���[�����̃f�o�t���󂯂Ă��Ȃ���ԂȂ�
            if (!onMoveDebuf)
            {
                ParticleSystem newPar = Instantiate(debufEffect);
                newPar.transform.position = playerC.PopObject.transform.position;
                newPar.Play();
            }
            jumpDebufIcon.enabled = true;
            //�����̌v�Z
            var def = 0.0f;
            if (jumpDebufMag > mag) def = jumpDebufMag - mag;
            else def = mag - jumpDebufMag;

            jumpDebufMag -= def;
            oldJumpDebufMag = jumpDebufMag;

            maxMoveDebufTime = time;
        }
        //�f�o�t�����擾�����{���̕��������ꍇ
        else if (onJumpDebuf)
        {
            //�{���̔�r
            var mag1 = oldJumpDebufMag;
            var mag2 = playerManager.DefJumpMag - mag;
            //����̔{�����O��̔{���������Ă���i���l�I�ɂ͏��������j�Ȃ�
            if (mag1 > mag2)
            {
                jumpDebufMag = mag2;
                //�t�^�����\��̌��ʎ��Ԃ��O��̕t�^���Ԃ������Ă���Ȃ�
                if (maxJumpDebufTime < time)
                {
                    //���s���Ԃ̃��Z�b�g
                    currentJumpDebufTime = 0.0f;
                    maxJumpDebufTime = time;
                }
            }
            //������Ă���Ȃ���ɉ������Ȃ�
            else jumpDebufMag = mag1;
        }

    }
    //���Ԍv���i�W�����v�p�j
    private void CountJumpDebuf()
    {
        currentJumpDebufTime += 0.01f;
        if(currentJumpDebufTime >= maxJumpDebufTime)
            EndJumpDebuf();
    }
    //�W�����v�f�o�t�̏I��
    private void EndJumpDebuf()
    {
        onJumpDebuf = false;
        jumpDebufIcon.enabled = false;
        jumpDebufMag = playerManager.DefJumpMag;
        currentJumpDebufTime = 0.0f;
        oldJumpDebufMag = 0.0f;
        maxJumpDebufTime = 0;
        if(!onJumpDebuf && !onMoveDebuf)
            Destroy(this.debufEffect);
    }
    #endregion
}

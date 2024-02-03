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
        jumpDebufIcon.enabled = false;
        moveDebufIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(onMoveDebuf)
            CountMoveDebuf();
        if(onJumpDebuf)
            CountJumpDebuf();
    }

    //�ړ��p�f�o�t�N��
    public void ActiveMoveDebuf(int time, float mag)
    {
        if (onMoveDebuf && oldMoveDebufMag < mag) moveDebufMag = 1.0f;
        else
        {
            onMoveDebuf = true;
            //�v���C���[�����̃f�o�t���󂯂Ă��Ȃ���ԂȂ�
            if (!onJumpDebuf)
            {
                ParticleSystem newPar = Instantiate(debufEffect);
                newPar.transform.position = playerC.PopObject.transform.position;
                newPar.Play();
            }
        }

        moveDebufIcon.enabled = true;
        moveDebufMag -= mag;
        oldMoveDebufMag = mag;
        maxMoveDebufTime = time;
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
        moveDebufMag = 1.0f;
        currentMoveDebufTime = 0.0f;
        oldMoveDebufMag = 0.0f;
        maxMoveDebufTime = 0;
        if (!onJumpDebuf && !onMoveDebuf)
            Destroy(this.debufEffect);
    }

    //�W�����v�p�f�o�t�N��
    public void ActiveJumpDebuf(int time, float mag)
    {
        if(onJumpDebuf && oldJumpDebufMag < mag) jumpDebufMag = 1.0f;
        else
        {
            onJumpDebuf = true;
            //�v���C���[�����̃f�o�t���󂯂Ă��Ȃ���ԂȂ�
            if (!onMoveDebuf)
            {
                ParticleSystem newPar = Instantiate(debufEffect);
                newPar.transform.position = playerC.PopObject.transform.position;
                newPar.Play();
            }
        }

        jumpDebufIcon.enabled = true;
        jumpDebufMag = jumpDebufMag - mag;
        oldJumpDebufMag = mag;
        maxJumpDebufTime = time;

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
        jumpDebufMag = 1.0f;
        currentJumpDebufTime = 0.0f;
        oldJumpDebufMag = 0.0f;
        maxJumpDebufTime = 0;
        if(!onJumpDebuf && !onMoveDebuf)
            Destroy(this.debufEffect);
    }

}

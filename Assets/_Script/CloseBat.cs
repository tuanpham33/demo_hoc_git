using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CloseBat : MonoBehaviour
{
    public GameObject bgBlackCua;
    public GameObject bgBlackNhen;
    public GameObject bgBlackRan;
    public GameObject bgBlackSoi;
    public GameObject bgBlackVoi;
    public GameObject bgBlackVuon;

    public TextMeshProUGUI textTienLai;
    public GameObject objTienLai;
    public int TienLaiSauTinhToan;
    public GameManager gameManager;
    public bool IsOpenning = false;
    public Animator animatorBat;
    // Start is called before the first frame update

    void Awake()
    {
        animatorBat = GetComponent<Animator>();
        if(animatorBat == null)
        {
            return;
        }
        if (gameManager == null) return;
    }

    public void SetAnimatorBatO()
    {
        if (IsOpenning == false)
        {
            if (gameManager.isBeting == false)
            {
                animatorBat.SetBool("IsOpening", true);
            }
            SetAnimatorBatXong();
        }
        else
        {
            animatorBat.SetBool("IsOpening", false);
            SetFalseBg();
            gameManager.isBeting1 = true;
            animatorBat.SetBool("XacXong", false);
        }
    }

    public void SetBatOTrue()
    {
        IsOpenning = true;
    }
    public void SetBatOFalse()
    {
        IsOpenning = false;
    }

    private void ThanhTien()
    {
        if (gameManager.isBeting1 == false)
        {
            bgBlackCua.SetActive(true);
            bgBlackNhen.SetActive(true);
            bgBlackRan.SetActive(true);
            bgBlackSoi.SetActive(true);
            bgBlackVoi.SetActive(true);
            bgBlackVuon.SetActive(true);

            Debug.Log("Trước khi tính tiền");
            gameManager.TinhTienTong();
            Debug.Log("Sau khi tính tiền");
            TienLaiSauTinhToan = gameManager.TienLai - gameManager.totalBet;
            if (TienLaiSauTinhToan > 0)
            {
                textTienLai.text = "Ván này +" + TienLaiSauTinhToan.ToString() + "$";
            }
            else
            {
                textTienLai.text = "Ván này " + TienLaiSauTinhToan.ToString() + "$";
            }
            Invoke("HienTien", 0.5f);
        }
    }

    public void HienTien()
    {
        
        SetTrueObj();
        gameManager.totalMoney += gameManager.TienLai;
        gameManager.texttotalMoney.text = "$" + gameManager.totalMoney.ToString();
        Invoke("SetFalseObj", 2f);
        gameManager.resetAllMoney();
    }
    
    private void SetFalseObj()
    {
        objTienLai.SetActive(false);
    }

    private void SetTrueObj()
    {
        objTienLai.SetActive(true);
    }

    private void SetFalseBg()
    {
        bgBlackCua.SetActive(false);
        bgBlackNhen.SetActive(false);
        bgBlackRan.SetActive(false);
        bgBlackSoi.SetActive(false);
        bgBlackVoi.SetActive(false);
        bgBlackVuon.SetActive(false);
    }

    public void SetAnimatorBatXong()
    {
        if (IsOpenning == false)
        {
            animatorBat.SetBool("XacXong", true);
            
        }
    }
}

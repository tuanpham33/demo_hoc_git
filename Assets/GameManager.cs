using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    // Số ô đoán chúng
    public int NWin;

    // Tiền lãi
    public int TienLai;
    public int m;

    // Số tiền đang có
    public int totalMoney = 10000;

    // Kiem tra xem co dang la thoi gian dat cuoc hay khong
    public bool isBeting;
    public bool isBeting1 = true;


    // PriceBet
    protected int priceBet;

    // Tổng tiền đặt cược
    public int totalBet;

    // Tiền đặt cược các ô
    protected int BetCua = 0;
    protected bool isBetCua;
    protected int BetNhen = 0;
    protected bool isBetNhen;
    protected int BetRan = 0;
    protected bool isBetRan;
    protected int BetSoi = 0;
    protected bool isBetSoi;
    protected int BetVoi = 0;
    protected bool isBetVoi;
    protected int BetVuon = 0;
    protected bool isBetVuon;

    // Không đủ tiền cược
    public GameObject NotEM;

    public Vector3 Pos1, Pos2, Pos3;
    public GameObject Cua, Nhen, Ran, Soi, Voi, Vuon;
    public int NRandom1, NRandom2, NRandom3;

    // Chữ hiện trên màn hình
    public TextMeshProUGUI texttotalMoney;
    public TextMeshProUGUI texttotalBet;
    public TextMeshProUGUI texttotalBetCua;
    public TextMeshProUGUI texttotalBetNhen;
    public TextMeshProUGUI texttotalBetRan;
    public TextMeshProUGUI texttotalBetSoi;
    public TextMeshProUGUI texttotalBetVoi;
    public TextMeshProUGUI texttotalBetVuon;

    public TextMeshProUGUI textTotalMSub;
    public TextMeshProUGUI textTotalBSub;

    public TextMeshProUGUI textAddMoneyCua;
    public TextMeshProUGUI textAddMoneyNhen;
    public TextMeshProUGUI textAddMoneyRan;
    public TextMeshProUGUI textAddMoneySoi;
    public TextMeshProUGUI textAddMoneyVoi;
    public TextMeshProUGUI textAddMoneyVuon;


    public TextMeshProUGUI textNotEnoughMoney;

    public GameObject textTMSub;
    public GameObject textTBSub;

    public GameObject textAddBetCuaObj;
    public GameObject textAddBetNhenObj;
    public GameObject textAddBetRanObj;
    public GameObject textAddBetSoiObj;
    public GameObject textAddBetVoiObj;
    public GameObject textAddBetVuonObj;

    // Nền đen của các ô cược
    public GameObject bgBlackCua;
    public GameObject bgBlackNhen;
    public GameObject bgBlackRan;
    public GameObject bgBlackSoi;
    public GameObject bgBlackVoi;
    public GameObject bgBlackVuon;

    // Component CloseBat
    public CloseBat closeBat = new CloseBat();

    // Animator Bat Dia
    public Animator animatorBat;

    public Vector3 poss1, poss2, poss3;
    public Vector3 newScale;

    protected void Awake()
    {
        totalMoney = PlayerPrefs.GetInt("TotalMoneyHaved");
        texttotalMoney.text = "$" + totalMoney.ToString();
        NotEM.SetActive(false);
        texttotalBet.text = totalBet.ToString();
        if (closeBat == null) return; 
    }
    public void RandomObject()
    {
        
        NRandom1 = Random.Range(1, 7);
        NRandom2 = Random.Range(1, 7);
        NRandom3 = Random.Range(1, 7);
        CheckObject(NRandom1, Pos1);
        Debug.Log("random");
        CheckObject(NRandom2, Pos2);
        CheckObject(NRandom3, Pos3);

    }
    private void CheckObject(int N, Vector3 Pos)
    {
        switch (N)
        {
            case 1:
                GameObject gameObject1 = Instantiate(this.Cua, Pos, Quaternion.identity);
                gameObject1.tag = "InBowl";
                break;
            case 2:
                GameObject gameObject2 = Instantiate(this.Nhen, Pos, Quaternion.identity);
                gameObject2.tag = "InBowl";
                break;
            case 3:
                GameObject gameObject3 = Instantiate(this.Ran, Pos, Quaternion.identity);
                gameObject3.tag = "InBowl";
                break;
            case 4:
                GameObject gameObject4 = Instantiate(this.Soi, Pos, Quaternion.identity);
                gameObject4.tag = "InBowl";
                break;
            case 5:
                GameObject gameObject5 = Instantiate(this.Voi, Pos, Quaternion.identity);
                gameObject5.tag = "InBowl";
                break;
            case 6:
                GameObject gameObject6 = Instantiate(this.Vuon, Pos, Quaternion.identity);
                gameObject6.tag = "InBowl";
                break;
        }
        Debug.Log("Switch");
    }
    private void DestroyBef()
    {
        // Tìm tất cả đối tượng có tag cụ thể
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("InBowl");

        // Hủy tất cả đối tượng có tag cụ thể
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

    public void StartGame()
    {
        if (closeBat.IsOpenning == false && isBeting == false)
        {
            // Random kết quả
            Invoke("RandomObject", 10f);

            // Animation Xắc
            animatorBat.SetBool("XacBat", true);
            Invoke("SetAnimationBat", 10f);

            // Xóa các kết quả cũ
            DestroyBef();

            // Debug
            Debug.Log("Bắt đầu game");

            // Bắt đầu thời gian đặt cược
            isBeting = true;

            // Hẹn giờ tính tiền
            Invoke("SetIsBetingFalse", 10f);

            // Đếm ngược
            StartCoroutine(StartCountdown());

            

        }
    }
    public void SetPrice1()
    {
        if (isBeting == true)
        {
            if ((totalMoney - 1) < 0)
            {
                AlarmNEM();
                priceBet = 0;
            }
            else
            {
                priceBet = 1;
            }
        }
    }
    public void SetPrice5()
    {
        if (isBeting == true)
        {
            if ((totalMoney - 5) < 0)
            {
                priceBet = 0;
                AlarmNEM();
            }
            else
            {
                priceBet = 5;
            }
        }
    }
    public void SetPrice10()
    {
        if (isBeting == true)
        {
            if ((totalMoney - 10) < 0)
            {
                priceBet = 0;
                AlarmNEM();
            }
            else
            {
                priceBet = 10;
            }
        }
    }
    public void SetPrice50()
    {
        if (isBeting == true)
        {
            if ((totalMoney - 50) < 0)
            {
                priceBet = 0;
                AlarmNEM();
            }
            else
            {
                priceBet = 50;
            }
        }
    }
    public void SetPrice100()
    {
        if (isBeting == true)
        {
            if ((totalMoney - 100) < 0)
            {
                priceBet = 0;
                AlarmNEM();
            }
            else
            {
                priceBet = 100;
            }
        }
    }
    public void SetPrice500()
    {
        if (isBeting == true)
        {
            if ((totalMoney - 500) < 0)
            {
                priceBet = 0;
                AlarmNEM();
            }
            else
            {
                priceBet = 500;
            }
        }
    }
    public void SetPrice1k()
    {
        if (isBeting == true)
        {
            if ((totalMoney - 1000) < 0)
            {
                priceBet = 0;
                AlarmNEM();
            }
            else
            {
                priceBet = 1000;
            }
        }
    }
    public void SetPrice5k()
    {
        if (isBeting == true)
        {
            if ((totalMoney - 5000) < 0)
            {
                priceBet = 0;
                AlarmNEM();
            }
            else
            {
                priceBet = 5000;
            }
        }
    }

    public void SetBetCua()
    {
        if((isBeting == true) && priceBet != 0)
        {
            if ((totalMoney - priceBet) < 0)
            {
                AlarmNEM();
                priceBet = 0;
            }
            else
            {
                textAddMoneyCua.text = "+" + priceBet.ToString() + "$";
                textAddBetCuaObj.SetActive(true);
                textTotalMoneySub(priceBet);
                textTotalBSub.text = "+" + priceBet.ToString() + "$";
                textTBSub.SetActive(true);
                BetCua += priceBet;
                texttotalBetCua.text = BetCua.ToString();
                isBetCua = true;
                UpdateTotalBetnMoney();
            }
        }    
    }
    public void SetBetNhen()
    {
        if ((isBeting == true) && priceBet != 0)
        {
            if ((totalMoney - priceBet) < 0)
            {
                AlarmNEM();
                priceBet = 0;
            }
            else
            {
                textAddMoneyNhen.text = "+" + priceBet.ToString() + "$";
                textAddBetNhenObj.SetActive(true);
                textTotalMoneySub(priceBet);
                textTotalBSub.text = "+" + priceBet.ToString() + "$";
                textTBSub.SetActive(true);
                BetNhen += priceBet;
                texttotalBetNhen.text = BetNhen.ToString();
                UpdateTotalBetnMoney();
                isBetNhen = true;
            }
        }
    }
    public void SetBetRan()
    {
        if ((isBeting == true) && priceBet != 0)
        {
            if ((totalMoney - priceBet) < 0)
            {
                AlarmNEM();
                priceBet = 0;
            }
            else
            {
                textAddMoneyRan.text = "+" + priceBet.ToString() + "$";
                textAddBetRanObj.SetActive(true);
                textTotalMoneySub(priceBet);
                textTotalBSub.text = "+" + priceBet.ToString() + "$";
                textTBSub.SetActive(true);
                BetRan += priceBet;
                texttotalBetRan.text = BetRan.ToString();

                UpdateTotalBetnMoney();
                isBetRan = true;
            }
        }
    }

    public void SetBetSoi()
    {
        if ((isBeting == true) && priceBet != 0)
        {
            if ((totalMoney - priceBet) < 0)
            {
                AlarmNEM();
                priceBet = 0;
            }
            else
            {
                textAddMoneySoi.text = "+" + priceBet.ToString() + "$";
                textAddBetSoiObj.SetActive(true);
                textTotalMoneySub(priceBet);
                textTotalBSub.text = "+" + priceBet.ToString() + "$";
                textTBSub.SetActive(true);
                BetSoi += priceBet;
                texttotalBetSoi.text = BetSoi.ToString();
                UpdateTotalBetnMoney();
                isBetSoi = true;
            }
        }
    }

    public void SetBetVoi()
    {
        if ((isBeting == true) && priceBet != 0)
        {
            if ((totalMoney - priceBet) < 0)
            {
                AlarmNEM();
                priceBet = 0;
            }
            else
            {
                textAddMoneyVoi.text = "+" + priceBet.ToString() + "$";
                textAddBetVoiObj.SetActive(true);
                textTotalMoneySub(priceBet);
                textTotalBSub.text = "+" + priceBet.ToString() + "$";
                textTBSub.SetActive(true);
                BetVoi += priceBet;
                texttotalBetVoi.text = BetVoi.ToString();
                UpdateTotalBetnMoney();
                isBetVoi = true;
            }
        }
    }

    public void SetBetVuon()
    {
        if ((isBeting == true) && priceBet != 0)
        {
            if ((totalMoney - priceBet) < 0)
            {
                AlarmNEM();
                priceBet = 0;
            }
            else
            {
                textAddMoneyVuon.text = "+" + priceBet.ToString() + "$";
                textAddBetVuonObj.SetActive(true);
                textTotalMoneySub(priceBet);
                textTotalBSub.text = "+" + priceBet.ToString() + "$";
                textTBSub.SetActive(true);
                BetVuon += priceBet;
                texttotalBetVuon.text = BetVuon.ToString();
                UpdateTotalBetnMoney();
                isBetVuon = true;
            }
        }
    }
    private void UpdateTotalBetnMoney()
    {
        totalBet += priceBet;
        texttotalBet.text = totalBet.ToString() +"$";
        totalMoney -= priceBet;
        texttotalMoney.text = "$" + totalMoney.ToString();
    }

    void AlarmNEM()
    {
        NotEM.SetActive(true);

        // Khởi tạo timer
        Invoke("SetActiveFalse", 0.5f);
    }
    private void SetActiveFalse()
    {
        // Set active false cho A
        NotEM.SetActive(false);
    }

    public void textTotalMoneySub(int N)
    {
        if(N != 0)
        {
            textTotalMSub.text = "-" + N.ToString() + "$";
            textTMSub.SetActive(true);
        }    

    }

    private void SetIsBetingFalse()
    {
        isBeting = false;
    }
    private void DestroyObj()
    {
        Destroy(gameObject);
    }

    public void TinhTien(int m, Vector3 pos)
    {
        switch(m)
        {
            case 1:
                TienLai += BetCua * 2;
                bgBlackCua.SetActive(false);
                if (isBetCua) NWin++;
                GameObject NewCua = Instantiate(this.Cua, pos, Quaternion.identity);
                NewCua.tag = "InKq";
                NewCua.transform.localScale = newScale;
                break;
            case 2:
                TienLai += BetNhen * 2;
                bgBlackNhen.SetActive(false);
                if (isBetNhen) NWin++;
                GameObject NewNhen = Instantiate(this.Nhen, pos, Quaternion.identity);
                NewNhen.tag = "InKq";
                NewNhen.transform.localScale = newScale;
                break;
            case 3:
                TienLai += BetRan * 2;
                bgBlackRan.SetActive(false);
                if (isBetRan) NWin++;
                GameObject NewRan = Instantiate(this.Ran, pos, Quaternion.identity);
                NewRan.tag = "InKq";
                NewRan.transform.localScale = newScale;
                break;
            case 4:
                TienLai += BetSoi * 2;
                bgBlackSoi.SetActive(false);
                if (isBetSoi) NWin++;
                GameObject NewSoi = Instantiate(this.Soi, pos, Quaternion.identity);
                NewSoi.tag = "InKq";
                NewSoi.transform.localScale = newScale;
                break;
            case 5:
                TienLai += BetVoi * 2;
                bgBlackVoi.SetActive(false);
                if (isBetVoi) NWin++;
                GameObject NewVoi = Instantiate(this.Voi, pos, Quaternion.identity);
                NewVoi.tag = "InKq";
                NewVoi.transform.localScale = newScale;
                break;
            case 6:
                TienLai += BetVuon * 2;
                bgBlackVuon.SetActive(false);
                if (isBetVuon) NWin++;
                GameObject NewVuon = Instantiate(this.Vuon, pos, Quaternion.identity);
                NewVuon.tag = "InKq";
                NewVuon.transform.localScale = newScale;
                break;
        }    
    }
    public void resetAllMoney()
    {
        priceBet = 0;
        BetCua = 0;
        isBetCua = false;
        BetNhen = 0;
        isBetNhen = false;
        BetRan = 0;
        isBetRan = false;
        BetSoi = 0;
        isBetSoi = false;
        BetVoi = 0;
        isBetVoi = false;
        BetVuon = 0;
        isBetVuon = false;
        totalBet = 0;
        TienLai = 0;
        texttotalBet.text = "0";
        texttotalBetCua.text = "0";
        texttotalBetNhen.text = "0";
        texttotalBetRan.text = "0";
        texttotalBetSoi.text = "0";
        texttotalBetVoi.text = "0";
        texttotalBetVuon.text = "0";
        NWin = 0;
    }

    public TextMeshProUGUI countdownText;
    private float countdownDuration = 10.0f;

    private IEnumerator StartCountdown()
    {
        float currentTime = countdownDuration;

        while (currentTime > 0)
        {
            UpdateCountdownText(currentTime);
            yield return new WaitForSeconds(1.0f);
            currentTime--;
        }

        // Đã hoàn thành đếm ngược, bạn có thể thực hiện hành động sau đây.
        countdownText.text = "0";
        isBeting1 = false;
    }

    private void UpdateCountdownText(float timeRemaining)
    {
        int seconds = Mathf.FloorToInt(timeRemaining);
        countdownText.text = seconds.ToString();
    }

    public void SetAnimationBat()
    {
        animatorBat.SetBool("XacBat", false);
    }

    public void TinhTienTong()
    {
        HienKetQua();
        TinhTien(NRandom1, poss1);
        TinhTien(NRandom2, poss2);
        TinhTien(NRandom3, poss3);
    }

    public void HienKetQua()
    {
        // Tìm tất cả các đối tượng có tag "InKq"
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("InKq");

        foreach (GameObject obj in objectsWithTag)
        {
            // Lấy vị trí hiện tại của đối tượng
            Vector3 currentPosition = obj.transform.position;
            currentPosition.x -= 0.72f;
            obj.transform.position = currentPosition;
            // Kiểm tra nếu vị trí x nhỏ hơn -7.5
            if (currentPosition.x < -7.7f)
            {
                // Xóa đối tượng nếu vị trí x nhỏ hơn -7.5
                Destroy(obj);
            }
        }
    }
}

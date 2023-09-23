using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DetailInfoUI : MonoBehaviour
{
    /// <summary>
    /// 아이템의 아이콘을 표시할 이미지
    /// </summary>
    Image itemIcon;

    /// <summary>
    /// 아이템의 이름을 출력할 텍스트
    /// </summary>
    TextMeshProUGUI itemName;

    /// <summary>
    /// 아이템의 가격을 출력할 텍스트
    /// </summary>
    TextMeshProUGUI itemPrice;

    /// <summary>
    /// 강화 전 아이템 오브젝트
    /// </summary>
    GameObject before;

    /// <summary>
    /// 강화 후 아이템 오브젝트
    /// </summary>
    GameObject after;

    /// <summary>
    /// 상승치의 아이템 오브젝트
    /// </summary>
    GameObject rising;

    /// <summary>
    /// 강화비용의 오브젝트
    /// </summary>
    GameObject cost;

    /// <summary>
    /// 아이템의 기존 스텟을 출력할 텍스트
    /// </summary>
    TextMeshProUGUI[] beforeStat;

    /// <summary>
    /// 아이템의 애프터 스텟을 출력할 텍스트
    /// </summary>
    TextMeshProUGUI[] afterStat;

    /// <summary>
    /// 아이템의 스텟 상승치를 출력할 텍스트
    /// </summary>
    TextMeshProUGUI[] risingStat;

    /// <summary>
    /// 코스트 수치를 적을 텍스트
    /// </summary>
    TextMeshProUGUI costText;

    /// <summary>
    /// 디테일창 전체의 알파를 조절할 캔버스 그룹
    /// </summary>
    CanvasGroup canvasGroup;

    /// <summary>
    /// 디테일창의 일시 정지 여부를 표시하는 변수
    /// </summary>
    bool isPause = false;

    /// <summary>
    /// 디테일 창이 열려있는지 확인하는 변수
    /// </summary>
    bool isOpen = false;

    /// <summary>
    /// 일시정지 여부를 확인 및 설정하는 프로퍼티
    /// </summary>
    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            if (isPause)
            {
                Close();    // 일시 정지가 되면 열려 있던 것도 닫는다.
            }
        }
    }

    /// <summary>
    /// 디테일창이 열리고 닫히는 속도
    /// </summary>
    public float alphaChangeSpeed = 10.0f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
        Transform child = transform.GetChild(0);
        itemIcon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        itemName = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        itemPrice = child.GetComponent<TextMeshProUGUI>();

        beforeStat = new TextMeshProUGUI[6];
        afterStat = new TextMeshProUGUI[6];
        risingStat = new TextMeshProUGUI[5];
        

        child = transform.GetChild(5);;
        before = child.gameObject;
        child = transform.GetChild(6);
        after = child.gameObject;
        child = transform.GetChild(7);
        rising = child.gameObject;
        child = transform.GetChild(8);
        cost = child.gameObject;

        for(int i = 0; i < 5; i++)
        {
            beforeStat[i] = before.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            afterStat[i] = after.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            risingStat[i] = rising.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
        beforeStat[5] = before.transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        afterStat[5] = after.transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        costText = cost.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 상세정보창을 여는 함수
    /// </summary>
    /// <param name="data">상세정보창에서 표시할 아이템의 데이터</param>
    public void Open(ItemData data)
    {
        if (!IsPause && data != null && isOpen == false)    // 일시정지 상태가 아니고, 아이템 데이터가 있고, 디테일 창이 다혀있을때만 열기
        {
            itemIcon.sprite = data.itemIcon;                // 아이콘 설정
            itemName.text = data.itemName;                  // 이름 설정
            itemPrice.text = data.price.ToString("N0");     // 가격 설정(3자리마다 콤마 추가)
            
            beforeStat[0].text = data.beforeStr.ToString();        // 강화 전 스텟(Str)
            beforeStat[1].text = data.beforeAgi.ToString();        // 강화 전 스텟(Agi)
            beforeStat[2].text = data.beforeInt.ToString();        // 강화 전 스텟(Int)
            beforeStat[3].text = data.beforeHP.ToString();         // 강화 전 스텟(HP)
            beforeStat[4].text = data.beforeMP.ToString();         // 강화 전 스텟(MP)
            beforeStat[5].text = data.beforeValue.ToString();      // 강화 전 강화수치

            afterStat[0].text = data.afterStr.ToString();          // 강화 후 스텟(Str)
            afterStat[1].text = data.afterAgi.ToString();          // 강화 후 스텟(Agi)
            afterStat[2].text = data.afterInt.ToString();          // 강화 후 스텟(Int)
            afterStat[3].text = data.afterHP.ToString();           // 강화 후 스텟(HP)
            afterStat[4].text = data.afterMP.ToString();           // 강화 후 스텟(MP)
            afterStat[5].text = data.afterValue.ToString();        // 강화 후 강화수치

            risingStat[0].text = $"+ {data.risingStr.ToString()}";        // 강화 시 상승 스텟(Str)
            risingStat[1].text = $"+ {data.risingAgi.ToString()}";        // 강화 시 상승 스텟(Agi)
            risingStat[2].text = $"+ {data.risingInt.ToString()}";        // 강화 시 상승 스텟(Int)
            risingStat[3].text = $"+ {data.risingHP.ToString()}";         // 강화 시 상승 스텟(HP)
            risingStat[4].text = $"+ {data.risingMP.ToString()}";         // 강화 시 상승 스텟(MP)

            costText.text = data.cost.ToString();                  // 강화 시 소모비용

            StopAllCoroutines();
            StartCoroutine(FadeIn());                       // 알파를 점점 1이 되도록 설정해서 보이게 만들기

            MovePosition(Mouse.current.position.ReadValue());   // 열릴 때 마우스 커서 위치에 열리기
            isOpen = true;
        }
    }

    /// <summary>
    /// 상세정보창을 닫는 함수
    /// </summary>
    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());  // 알파를 점점 0이 되도록 설정해서 안보이게 만들기
        isOpen = false;
    }

    /// <summary>
    /// 상세정보창을 움직이는 함수
    /// </summary>
    /// <param name="screenPos">위치할 스크린 좌표</param>
    public void MovePosition(Vector2 screenPos)
    {
        if (canvasGroup.alpha > 0.0f)      // 보이는 상황일 때만
        {
            RectTransform rectTransform = (RectTransform)transform;     // rectTransform 가져오기

            int overX = (int)(screenPos.x + rectTransform.sizeDelta.x) - Screen.width;  // 화면 밖으로 넘친 정도를 계산
            overX = Mathf.Max(0, overX);        // 음수 제거(음수면 정상 범위)
            screenPos.x -= overX;               // 넘친만큼 왼쪽으로 이동시키기

            int overY = (int)(screenPos.y + rectTransform.sizeDelta.y) - Screen.height;  // 화면 밖으로 넘친 정도를 계산
            overY = Mathf.Max(0, overY);        // 음수 제거(음수면 정상 범위)
            screenPos.y -= overY;               // 넘친만큼 왼쪽으로 이동시키기


            transform.position = new Vector3(screenPos.x + 320, screenPos.y + 280);     // 이동 시키기
        }
    }

    public void UpgradeButtonClick()
    {
        // 아이템 강화 함수
        Debug.Log("아이템 업그레이드 시도");
    }

    public void SellButtonClick()
    {
        // 아이템 판매 함수
        Debug.Log("아이템 판매");
    }

    /// <summary>
    /// 디테일창을 점점 보이게 만드는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1.0f)    // 알파가 1이 될 때까지 매 프레임마다 조금씩 증가
        {
            canvasGroup.alpha += Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    /// <summary>
    /// 디테일창이 점점 안보이게 만드는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0.0f)    // 알파가 0이 될 때까지 매프레임마다 조금씩 감소
        {
            canvasGroup.alpha -= Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
    }
}

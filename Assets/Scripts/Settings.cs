using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject panel_set;  // 패널을 드래그해서 연결할 변수

    public GameObject panel_info;

    public GameObject home;

    public GameObject union;

    public GameObject book;

    public GameObject friends;

    public GameObject camera;

    public GameObject dairy;

    public GameObject gacha;

    

    // 버튼을 눌렀을 때 패널을 보이게 하는 메서드
    // set클릭
    public void ShowSetPanel()
    {
        panel_set.SetActive(true);  // 패널을 활성화하여 메인 화면을 가림
    }

    public void HideSetPanel()
    {
        panel_set.SetActive(false);  // 패널을 비활성화하여 메인 화면을 보이게 함
    }


    // info클릭
        public void ShowInfoPanel()
    {
        panel_info.SetActive(true);
    }

    public void HideInfoPanel()
    {
        panel_info.SetActive(false);
    }


    // home버튼 클릭
    public void HomeBtn()
    {
        panel_info.SetActive(true);
    }


    // 업적 버튼
    public void UnionBtn()
    {
        union.SetActive(true);
    }

    public void HideUnion()
    {
        union.SetActive(false);
    }


    // 도감 버튼
    public void BookBtn()
    {
        book.SetActive(true);
    }

    public void HideBook()
    {
        book.SetActive(false);
    }


    // 친구 버튼
    public void FriendsBtn()
    {
        friends.SetActive(true);
    }

    public void HideFriends()
    {
        friends.SetActive(false);
    }


    //카메라 키기
    public void OpenCamera()
    {
        camera.SetActive(true);
    }

    public void HideCamera()
    {
        camera.SetActive(false);
    }



    public void OpenDairy()
    {
        dairy.SetActive(true);
    }

    public void HideDairy()
    {
        dairy.SetActive(false);
    }


    public void Showgacha()
    {
        gacha.SetActive(true);
    }

    public void Hidegacha()
    {
        gacha.SetActive(false);
    }
}

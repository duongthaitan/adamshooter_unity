using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer; // Tham chiếu đến UI Text để hiển thị thời gian
    int gameMode = 0;  // Biến lưu trữ chế độ chơi (0 hoặc 1) 
    public int timer;   // Biến đếm thời gian

    private void Start()
    {
        // Lấy giá trị "gameMode" đã lưu từ PlayerPrefs (chế độ chơi khi game bắt đầu)
        gameMode = PlayerPrefs.GetInt("gameMode");

        // Bắt đầu coroutine (tiến trình đếm thời gian)
        StartCoroutine(StartTimer());
    }

    // Coroutine để thực hiện đếm thời gian
    IEnumerator StartTimer()
    {
        int showTimer = 0;  // Biến lưu trữ thời gian hiển thị trên UI
        int maxTimer = 0;   // Thời gian tối đa của chế độ chơi
        if (gameMode == 0) maxTimer = 1800; // Nếu là chế độ 0, thời gian chơi tối đa là 30 phút (1800 giây)
        int second, minute;  // Biến lưu trữ số giây và phút trong thời gian hiển thị

        while (true)
        {
            timer++; // Tăng giá trị thời gian mỗi giây

            if (gameMode == 0)  // Chế độ 0 (chế độ đếm ngược)
            {
                showTimer = maxTimer - timer;  // Tính thời gian còn lại
                if (timer >= maxTimer)  // Nếu hết thời gian
                {
                    // Xử lý khi thắng cuộc (hoặc hết thời gian)
                }
            }
            else  // Chế độ 1 (Chế độ thời gian vô hạn)
            {
                showTimer = timer;  // Chỉ hiển thị đếm thời gian đến khi người chơi chết
            }

            // Tính phút và giây từ tổng số giây
            second = showTimer % 60;
            minute = (showTimer / 60) % 60;

            // Cập nhật giá trị trên UI text
            textTimer.text = minute.ToString() + ":" + second.ToString();

            // Chờ 1 giây trước khi tiếp tục
            yield return new WaitForSeconds(1f);
        }
    }
}

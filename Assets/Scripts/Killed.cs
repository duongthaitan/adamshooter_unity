using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Thư viện để sử dụng TextMeshPro

public class Killed : MonoBehaviour
{
    // Tham chiếu đến đối tượng TextMeshProUGUI để hiển thị số kẻ thù đã tiêu diệt
    public TextMeshProUGUI text;

    // Biến lưu trữ số lượng kẻ thù hiện tại mà người chơi đã tiêu diệt
    public int currentKilled = 0;

    // Hàm chạy khi bắt đầu trò chơi
    private void Start()
    {
        // Gán giá trị ban đầu cho văn bản hiển thị (bắt đầu từ 0)
        text.text = "0";
    }

    // Hàm cập nhật số lượng kẻ thù bị tiêu diệt
    public void UpdateKilled()
    {
        // Tăng số lượng kẻ thù bị tiêu diệt lên 1
        currentKilled++;

        // Cập nhật văn bản để hiển thị số lượng kẻ thù mới
        text.text = currentKilled.ToString();
    }
}

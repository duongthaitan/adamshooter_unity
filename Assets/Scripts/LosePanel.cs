using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; // Thư viện để quản lý và tải lại các cảnh (scene)

public class LosePanel : MonoBehaviour
{
    // Văn bản hiển thị điểm số trên bảng thua cuộc
    public TextMeshProUGUI score;

    private void Start()
    {
        // Ẩn bảng thua cuộc khi trò chơi bắt đầu
        Hide();
    }

    // Hàm hiển thị bảng thua cuộc
    public void Show()
    {
        // Kích hoạt bảng thua cuộc
        gameObject.SetActive(true);

        // Tính điểm dựa trên số kẻ thù đã tiêu diệt (mỗi kẻ thù cho 10 điểm)
        int scoreI = FindObjectOfType<Killed>().currentKilled * 10;

        // Cập nhật văn bản hiển thị điểm số
        score.text = "Bạn có: " + scoreI.ToString() + " Điểm";

        // Dừng thời gian trong trò chơi (tạm ngừng mọi hoạt động)
        Time.timeScale = 0;
    }

    // Hàm ẩn bảng thua cuộc
    public void Hide()
    {
        // Khôi phục thời gian trong trò chơi (tiếp tục hoạt động)
        Time.timeScale = 1;

        // Ẩn bảng thua cuộc
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím "R" để chơi lại
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Khôi phục thời gian về bình thường trước khi tải lại cảnh
            Time.timeScale = 1;

            // Tải lại cảnh hiện tại
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

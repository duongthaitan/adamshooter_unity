using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    // Thanh hiển thị kinh nghiệm (Exp Bar)
    public HealthBar ExpBar;

    // Kinh nghiệm hiện tại của người chơi
    int currentExp = 0;

    // Cấp độ hiện tại của người chơi
    int currentLevel = 1;

    // Kinh nghiệm cần thiết để lên cấp
    int requireExp = 30;

    // Panel hiển thị khi người chơi lên cấp
    public GameObject levelUpPanel;

    // Hàm cập nhật kinh nghiệm và xử lý lên cấp
    public void UpdateExperience(int addExp)
    {
        // Cộng thêm kinh nghiệm mới vào kinh nghiệm hiện tại
        currentExp += addExp;

        // Kiểm tra nếu đủ kinh nghiệm để lên cấp
        if (currentExp >= requireExp)
        {
            // Tăng cấp độ
            currentLevel++;

            // Cập nhật kinh nghiệm hiện tại bằng cách trừ đi kinh nghiệm cần thiết
            currentExp = currentExp - requireExp;

            // Tăng kinh nghiệm cần thiết để lên cấp tiếp theo (tăng gấp đôi)
            requireExp = (int)(requireExp * 2);

            // Mở bảng lên cấp
            OpenLevelUpPanel();
        }

        // Cập nhật trạng thái của thanh Exp Bar
        ExpBar.UpdateBar(currentExp, requireExp, "Level " + currentLevel.ToString());
    }

    // Hàm đóng  lên cấp
    public void CloseLevelUpPanel()
    {
        // Lấy CanvasGroup để điều chỉnh hiển thị bảng
        CanvasGroup group = levelUpPanel.GetComponent<CanvasGroup>();

        // Ẩn bảng bằng cách đặt alpha (độ trong suốt) về 0
        group.alpha = 0;

        // Vô hiệu hóa tương tác và chặn raycast
        group.blocksRaycasts = false;
        group.interactable = false;

        // Tiếp tục thời gian trong game
        Time.timeScale = 1;
    }

    // Hàm mở bảng lên cấp
    public void OpenLevelUpPanel()
    {
        // Lấy CanvasGroup để điều chỉnh hiển thị bảng
        CanvasGroup group = levelUpPanel.GetComponent<CanvasGroup>();

        // Hiển thị bảng bằng cách đặt alpha (độ trong suốt) về 1
        group.alpha = 1;

        // Kích hoạt tương tác và cho phép raycast
        group.blocksRaycasts = true;
        group.interactable = true;

        // Dừng thời gian trong game để người chơi chọn các tùy chọn lên cấp
        Time.timeScale = 0;
    }
}

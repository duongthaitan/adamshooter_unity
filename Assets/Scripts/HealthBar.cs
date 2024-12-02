using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Thư viện để sử dụng TextMeshPro
using UnityEngine.UI; 

public class HealthBar : MonoBehaviour
{
    // Hiển thị văn bản trên thanh máu hoặc thanh tiến trình (ví dụ: "50 / 100")
    public TextMeshProUGUI healthText;

    // Thêm biến  Image bar cho thanh máu
    public Image bar;

    // Cập nhật thanh máu
    public void UpdateHealth(int health, int maxHealth)
    {
        // Hiển thị thông tin thanh máu dưới dạng "hiện tại / tối đa" (ví dụ: "50 / 100")
        healthText.text = health.ToString() + " / " + maxHealth.ToString();

        // Cập nhật chiều dài của thanh dựa trên tỷ lệ giữa máu hiện tại và tối đa
        bar.fillAmount = (float)health / (float)maxHealth;
    }

    // Cập nhật một thanh tiến trình bất kỳ (có thể là kinh nghiệm, năng lượng, v.v.)
    public void UpdateBar(int value, int maxValue, string text)
    {
        // Hiển thị thông tin văn bản tuỳ chỉnh trên thanh máu (ví dụ: "Level 2")
        healthText.text = text;

        // Cập nhật chiều dài của thanh dựa trên tỷ lệ giữa giá trị hiện tại và giá trị tối đa
        bar.fillAmount = (float)value / (float)maxValue;
    }
}

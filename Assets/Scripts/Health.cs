using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Máu tối đa của người chơi
    public int maxHealth;

    // Máu hiện tại, được ẩn trong Inspector (chỉ quản lý bằng code)
    [HideInInspector] public int currentHealth;

    // Thanh máu để hiển thị trạng thái máu
    public HealthBar healthBar;

    // Thời gian bất tử sau khi hp về 0 sau khi bị tấn công 
    private float safeTime;
    public float safeTimeDuration = 0f;

    // Biến kiểm tra xem người chơi đã chết hay chưa
    public bool isDead = false;

    // Biến tùy chọn để kích hoạt rung camera khi nhận sát thương
    public bool camShake = false;

    // Hàm khởi tạo, được gọi khi người chơi bắt đầu hoạt động
    private void Start()
    {
        // Gán máu hiện tại bằng máu tối đa khi bắt đầu
        currentHealth = maxHealth;

        // Cập nhật thanh máu khi được thiết lập
        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    // Hàm xử lý khi đối tượng nhận sát thương
    public void TakeDam(int damage)
    {
        // Chỉ nhận sát thương khi hết thời gian bất tử
        if (safeTime <= 0)
        {
            // Trừ máu hiện tại đi bằng số giá trị sát thương nhận
            currentHealth -= damage;

            // Nếu máu bằng hoặc nhỏ hơn 0, xử lý cái chết
            if (currentHealth <= 0)
            {
                currentHealth = 0;

                // Nếu đối tượng là kẻ thù
                if (this.gameObject.tag == "Enemy")
                {
                    // Xóa kẻ thù khỏi phạm vi tấn công của vũ khí
                    FindObjectOfType<WeaponManager>().RemoveEnemyToFireRange(this.transform);

                    // Cập nhật số kẻ thù bị tiêu diệt
                    FindObjectOfType<Killed>().UpdateKilled();

                    // Cập nhật kinh nghiệm cho người chơi (ngẫu nhiên từ 1 đến 3)
                    FindObjectOfType<PlayerExp>().UpdateExperience(UnityEngine.Random.Range(1, 4));

                    // Xóa đối tượng kẻ thù sau một khoảng thời gian ngắn (0.125 giây)
                    Destroy(this.gameObject, 0.125f);
                }

                // Đặt trạng thái đối tượng thành "đã chết"
                isDead = true;
            }

            // Nếu đối tượng có thanh máu, cập nhật trạng thái thanh máu
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            // Thiết lập lại thời gian bất tử
            safeTime = safeTimeDuration;
        }
    }

    // Hàm được gọi mỗi frame
    private void Update()
    {
        // Giảm thời gian bất tử theo thời gian thực
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro; 
using Unity.VisualScripting; 
using UnityEngine;

public class Player : MonoBehaviour
{
    // Tốc độ di chuyển của người chơi
    public float moveSpeed = 5f;

    // Thành phần Rigidbody2D để xử lý vật lý 2D
    public Rigidbody2D rb;

    // SpriteRenderer để thay đổi hướng người chơi
    public SpriteRenderer characterSR;

    // Animator để xử lý hoạt ảnh
    Animator animator;

    // Các biến liên quan đến khả năng Dash
    public float dashBoost = 2f; // Tăng tốc khi Dash -> 
    private float dashTime; // Thời gian hiện tại của Dash
    public float DashTime; // Thời gian Dash tối đa
    private bool once; // Để đảm bảo việc Dash chỉ diễn ra một lần

    // Hướng di chuyển của người chơi
    public Vector3 moveInput;

    // Hiệu ứng hiển thị sát thương
    public GameObject damPopUp;

    // Bảng thua cuộc khi người chơi chết
    public LosePanel losePanel;

    private void Start()
    {
        // Lấy thành phần Rigidbody2D từ chính đối tượng này
        rb = GetComponent<Rigidbody2D>();

        // Lấy Animator từ đối tượng con chứa hoạt ảnh
        animator = GetComponentInChildren<Animator>();
    }

    // Hàm được gọi mỗi khung hình
    void Update()
    {
        /// Phần 1: Xử lý di chuyển
        // Nhận đầu vào bàn phím từ các phím mũi tên hoặc A/D (trục ngang) và W/S (trục dọc)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Cập nhật vị trí của người chơi dựa trên hướng di chuyển và tốc độ
        transform.position += moveSpeed * Time.deltaTime * moveInput;

        // Cập nhật giá trị tốc độ cho Animator để chuyển hoạt ảnh
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        /// Phần 2: Xử lý Dash
        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            // Khi nhấn phím Space và Dash sẵn sàng, kích hoạt hoạt ảnh Roll
            animator.SetBool("Roll", true);

            // Tăng tốc độ di chuyển khi Dash
            moveSpeed += dashBoost;

            // Đặt thời gian Dash bằng giá trị tối đa
            dashTime = DashTime;

            // Ghi nhận rằng Dash đã được kích hoạt
            once = true;
        }

        // Xử lý khi Dash kết thúc
        if (dashTime <= 0 && once)
        {
            // Tắt hoạt ảnh Roll
            animator.SetBool("Roll", false);

            // Giảm tốc độ trở lại bình thường
            moveSpeed -= dashBoost;

            // Đặt trạng thái Dash về chưa kích hoạt
            once = false;
        }
        else
        {
            // Giảm dần thời gian Dash
            dashTime -= Time.deltaTime;
        }

        /// Phần 3: Xử lý hướng người chơi
        if (moveInput.x != 0)
        {
            // Nếu người chơi di chuyển trái, quay mặt về bên trái
            if (moveInput.x < 0)
                characterSR.transform.localScale = new Vector3(-1, 1, 0);
            else
                // Nếu di chuyển phải, quay mặt về bên phải
                characterSR.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    // Hàm xử lý khi nhận sát thương
    public void TakeDamageEffect(int damage)
    {
        if (damPopUp != null)
        {
            // Hiển thị popup sát thương tại vị trí của người chơi
            GameObject instance = Instantiate(damPopUp, transform.position
                    + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);

            // Cập nhật giá trị sát thương lên popup
            instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();

            // Kích hoạt hiệu ứng hoạt ảnh "red" trên popup
            Animator animator = instance.GetComponentInChildren<Animator>();
            animator.Play("red");
        }

        // Nếu người chơi chết (isDead = true), hiển thị bảng thua cuộc
        if (GetComponent<Health>().isDead)
        {
            losePanel.Show();
        }
    }
}

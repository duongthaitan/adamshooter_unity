using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Đạn sẽ được bắn ra
    public GameObject projectile;

    // Hiệu ứng lửa tại nòng súng khi bắn
    public GameObject muzzle;

    // Các vị trí đạn bắn ra (nếu súng có nhiều nòng hoặc hướng)
    public Transform[] spawnPos;

    // Thời gian giữa các lần bắn
    private float timeBtwShots;

    // Thời gian mặc định giữa hai lần bắn
    public float startTimeBtwShots;

    // Lực bắn của viên đạn
    public float bulletForce;

    // Sát thương tối thiểu và tối đa của viên đạn
    public int minDamage = 6;
    public int maxDamage = 16;

    // Hiệu ứng khi súng bắn (ánh sáng, khói, v.v.)
    public GameObject fireEffect;

    // Quản lý vũ khí (tìm kiếm và điều khiển các kẻ thù)
    public WeaponManager weaponManager;

    // Điểm gốc để tính toán góc bắn
    public Transform calculatePoint;

    // Khởi tạo các biến và tham chiếu khi bắt đầu trò chơi
    private void Start()
    {
        // Tìm đối tượng quản lý vũ khí trong cảnh
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    private void Update()
    {
        // Giảm thời gian giữa các lần bắn dần theo thời gian
        timeBtwShots -= Time.deltaTime;

        // Nếu hết thời gian chờ, súng có thể bắn
        if (timeBtwShots <= 0)
        {
            // Tìm kẻ thù gần nhất dựa trên vị trí của `calculatePoint`
            Transform enemy = weaponManager.FindNearestEnemy(calculatePoint.position);

            // Nếu tìm thấy kẻ thù
            if (enemy != null)
            {
                // Xoay súng hướng về phía kẻ thù
                RotateGun(enemy.position);

                // Thực hiện bắn
                Fire();
            }
        }
    }

    // Xoay súng về phía mục tiêu
    void RotateGun(Vector3 pos)
    {
        // Tính toán hướng từ súng đến mục tiêu
        Vector2 lookDir = pos - transform.position;

        // Chuyển hướng thành góc (đơn vị độ)
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // Tạo góc quay dựa trên góc tính được
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Xoay súng theo góc quay
        transform.rotation = rotation;

        // Điều chỉnh hướng xoay của súng 
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            transform.localScale = new Vector3(1, -1, 0); // Lật súng
        else
            transform.localScale = new Vector3(1, 1, 0);  // Giữ nguyên
    }

    // Hàm thực hiện bắn đạn
    void Fire()
    {
        // Duyệt qua tất cả các vị trí bắn (nếu có nhiều nòng súng)
        foreach (Transform spawn in spawnPos)
        {
            // Tạo hiệu ứng nòng súng bắn tại vị trí
            Instantiate(muzzle, spawn.position, transform.rotation, transform);

            // Tạo viên đạn tại vị trí bắn
            var bullet = Instantiate(projectile, spawn.position, Quaternion.identity);

            // Truyền sát thương tối thiểu và tối đa cho đạn
            Bullet bulletC = bullet.GetComponent<Bullet>();
            bulletC.minDamage = minDamage;
            bulletC.maxDamage = maxDamage;

            // Reset thời gian giữa các lần bắn
            timeBtwShots = startTimeBtwShots;

            // Thêm lực cho đạn để nó bắn ra
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

            // Tạo hiệu ứng lửa bắn ra tại nòng súng
            var fireE = Instantiate(fireEffect, spawn.position, transform.rotation, transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Danh sách các slot vũ khí
    public List<Transform> weaponSlots = new List<Transform>();

    // Chỉ số của slot vũ khí hiện tại
    int currentWeaponSlot = 0;

    // Hàm thêm vũ khí vào slot
    public void AddWeapon(GameObject weaponPrefab)
    {
        // Nếu còn slot trống
        if (currentWeaponSlot < weaponSlots.Count)
        {
            // Tạo một vũ khí mới tại slot hiện tại
            Instantiate(weaponPrefab, weaponSlots[currentWeaponSlot]);

            // Chuyển sang slot tiếp theo
            currentWeaponSlot++;
        }
    }

    // Danh sách các kẻ thù nằm trong phạm vi bắn
    public List<Transform> Enemies = new List<Transform>();

    // Thêm kẻ thù vào phạm vi bắn
    public void AddEnemyToFireRange(Transform transform)
    {
        // Trạng thái máu của kẻ thù
        Health enemyHealth = transform.GetComponent<Health>();

        // Nếu kẻ thù chưa chết thì thêm vào danh sách
        if (!enemyHealth.isDead)
            Enemies.Add(transform);
    }

    // Xóa kẻ thù ra khỏi phạm vi bắn
    public void RemoveEnemyToFireRange(Transform transform)
    {
        Enemies.Remove(transform); // Xóa kẻ thù ra khỏi danh sách
    }

    // Tìm kẻ thù gần nhất so với vị trí của vũ khí
    public Transform FindNearestEnemy(Vector2 weaponPos)
    {
        // Nếu không có kẻ thù nào trong danh sách, trả về null
        if (Enemies != null && Enemies.Count <= 0) return null;

        // Ví dụ kẻ thù đầu tiên là gần nhất
        Transform nearestEnemy = Enemies[0];

        // Duyệt qua tất cả các kẻ thù trong danh sách
        foreach (Transform enemy in Enemies)
        {
            // So sánh khoảng cách giữa kẻ thù hiện tại và kẻ thù gần nhất
            if (Vector2.Distance(enemy.position, weaponPos) < Vector2.Distance(nearestEnemy.position, weaponPos))
                nearestEnemy = enemy; // Cập nhật kẻ thù gần nhất
        }

        return nearestEnemy; // Trả về
    }
}

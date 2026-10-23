using UnityEngine;
using UnityEngine.InputSystem; // เพิ่ม namespace สำหรับ New Input System

public class FXContainer : MonoBehaviour
{
    [Header("FX Settings")]
    [Tooltip("ลาก FX Prefab จากโฟลเดอร์ Assets มาใส่ในช่องนี้")]
    public GameObject fxPrefab;

    [Tooltip("ระยะเวลา (วินาที) ที่จะลบ FX ออกจากฉากอัตโนมัติ")]
    public float autoDestroyDelay = 2.0f;

    [Header("Camera Reference")]
    [Tooltip("หากปล่อยว่างไว้ สคริปต์จะใช้ Camera.main โดยอัตโนมัติ")]
    public Camera targetCamera;

    private void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void Update()
    {
        // ตรวจสอบการคลิกเมาส์ซ้ายด้วย New Input System
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnFXAtMousePosition();
        }
    }

    private void SpawnFXAtMousePosition()
    {
        if (fxPrefab == null)
        {
            Debug.LogWarning("[ClickToSpawnFX] ลืมใส่ FX Prefab ใน Inspector ครับ!", this);
            return;
        }

        if (targetCamera == null)
        {
            Debug.LogError("[ClickToSpawnFX] ไม่พบ Main Camera ในฉาก!", this);
            return;
        }

        // ดึงตำแหน่งเมาส์ด้วย New Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseScreenPos = new Vector3(mousePos.x, mousePos.y, Mathf.Abs(targetCamera.transform.position.z));

        Vector3 spawnWorldPos = targetCamera.ScreenToWorldPoint(mouseScreenPos);

        // สร้าง FX Prefab ตรงตำแหน่งที่คลิก
        GameObject spawnedFX = Instantiate(fxPrefab, spawnWorldPos, Quaternion.identity);

        // เล่น Particle System (ถ้ามี)
        ParticleSystem ps = spawnedFX.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }

        // เล่นเสียง (ถ้ามี)
        AudioSource audio = spawnedFX.GetComponentInChildren<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }

        // ทำลาย Object ทิ้งอัตโนมัติ
        if (autoDestroyDelay > 0)
        {
            Destroy(spawnedFX, autoDestroyDelay);
        }
    }
}
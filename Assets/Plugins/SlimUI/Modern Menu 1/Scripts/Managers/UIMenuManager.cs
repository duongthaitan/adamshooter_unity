using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;
    using TMPro;
    using UnityEngine.SceneManagement;

    namespace SlimUI.ModernMenu
    {
        public class UIMenuManager : MonoBehaviour
        {
            private Animator CameraObject; // Animator cho camera, điều khiển hiệu ứng chuyển động

            // Các menu trong game
            [Header("MENUS")]
            public GameObject mainMenu; // Menu chính
            public GameObject firstMenu; // Menu đầu tiên
            public GameObject playMenu; // Menu chơi game
            public GameObject exitMenu; // Menu thoát game
            public GameObject extrasMenu; // Menu phụ

            public enum Theme { custom1, custom2, custom3 }; // Các chủ đề giao diện
            [Header("THEME SETTINGS")]
            public Theme theme; // Chọn chủ đề
            private int themeIndex; // Chủ đề đã chọn
            public ThemedUIData themeController; // Điều khiển màu sắc giao diện

            // Các panel UI cho game
            [Header("PANELS")]
            public GameObject mainCanvas; // Canvas chính chứa các menu
            public GameObject customCanvas; // Canvas tùy chỉnh
            public GameObject PanelControls; // Panel điều khiển
            public GameObject PanelVideo; // Panel video
            public GameObject PanelGame; // Panel game
            public GameObject PanelMovement; // Panel di chuyển
            public GameObject PanelCombat; // Panel chiến đấu
            public GameObject PanelGeneral; // Panel chung

            // Màn hình cài đặt
            [Header("SETTINGS SCREEN")]
            public GameObject lineGame; // Dấu hiệu khi tab GAME được chọn
            public GameObject lineVideo; // Dấu hiệu khi tab VIDEO được chọn
            public GameObject lineControls; // Dấu hiệu khi tab CONTROLS được chọn
            public GameObject lineKeyBindings; // Dấu hiệu khi tab KEY BINDINGS được chọn
            public GameObject lineMovement; // Dấu hiệu khi tab MOVEMENT được chọn trong KEY BINDINGS
            public GameObject lineCombat; // Dấu hiệu khi tab COMBAT được chọn trong KEY BINDINGS
            public GameObject lineGeneral; // Dấu hiệu khi tab GENERAL được chọn trong KEY BINDINGS

            // Màn hình loading
            [Header("LOADING SCREEN")]
            public bool waitForInput = true; // Chờ người dùng nhập liệu để tiếp tục
            public GameObject loadingMenu; // Màn hình loading
            public Slider loadingBar; // Thanh loading
            public TMP_Text loadPromptText; // Văn bản yêu cầu nhập liệu
            public KeyCode userPromptKey; // Phím yêu cầu nhập liệu

            // SFX (âm thanh)
            [Header("SFX")]
            public AudioSource hoverSound; // Âm thanh khi hover vào nút
            public AudioSource sliderSound; // Âm thanh cho slider
            public AudioSource swooshSound; // Âm thanh chuyển giữa các màn hình

            void Start()
            {
                CameraObject = transform.GetComponent<Animator>(); // Lấy Animator từ Camera

                // Thiết lập các menu ban đầu
                playMenu.SetActive(false);
                exitMenu.SetActive(false);
                if (extrasMenu) extrasMenu.SetActive(false);
                firstMenu.SetActive(true);
                mainMenu.SetActive(true);

                SetThemeColors(); // Gọi hàm để thiết lập màu sắc chủ đề
            }

            // Hàm để thiết lập màu sắc giao diện theo chủ đề đã chọn
            void SetThemeColors()
            {
                switch (theme)
                {
                    case Theme.custom1:
                        themeController.currentColor = themeController.custom1.graphic1;
                        themeController.textColor = themeController.custom1.text1;
                        themeIndex = 0;
                        break;
                    case Theme.custom2:
                        themeController.currentColor = themeController.custom2.graphic2;
                        themeController.textColor = themeController.custom2.text2;
                        themeIndex = 1;
                        break;
                    case Theme.custom3:
                        themeController.currentColor = themeController.custom3.graphic3;
                        themeController.textColor = themeController.custom3.text3;
                        themeIndex = 2;
                        break;
                    default:
                        Debug.Log("Invalid theme selected."); // Nếu không có chủ đề hợp lệ
                        break;
                }
            }

            // Các hàm điều khiển các menu trong game
            public void PlayCampaign()
            {
                exitMenu.SetActive(false);
                if (extrasMenu) extrasMenu.SetActive(false);
                playMenu.SetActive(true);
            }

            public void PlayCampaignMobile()
            {
                exitMenu.SetActive(false);
                if (extrasMenu) extrasMenu.SetActive(false);
                playMenu.SetActive(true);
                mainMenu.SetActive(false);
            }

            public void ReturnMenu()
            {
                playMenu.SetActive(false);
                if (extrasMenu) extrasMenu.SetActive(false);
                exitMenu.SetActive(false);
                mainMenu.SetActive(true);
            }

            // Hàm để load scene khi chọn chơi
            public void LoadScene(string scene)
            {
                if (scene != "")
                {
                    StartCoroutine(LoadAsynchronously(scene)); // Tải scene không đồng bộ
                }
            }

            // Tắt menu PlayCampaign
            public void DisablePlayCampaign()
            {
                playMenu.SetActive(false);
            }

            // Các hàm điều khiển camera
            public void Position2()
            {
                DisablePlayCampaign();
                CameraObject.SetFloat("Animate", 1); // Chuyển camera đến vị trí 2
            }

            public void Position1()
            {
                CameraObject.SetFloat("Animate", 0); // Quay lại vị trí 1
            }

            // Hàm tắt các panel UI
            void DisablePanels()
            {
                PanelControls.SetActive(false);
                PanelVideo.SetActive(false);
                PanelGame.SetActive(false);

                lineGame.SetActive(false);
                lineControls.SetActive(false);
                lineVideo.SetActive(false);
                lineKeyBindings.SetActive(false);

                PanelMovement.SetActive(false);
                lineMovement.SetActive(false);
                PanelCombat.SetActive(false);
                lineCombat.SetActive(false);
                PanelGeneral.SetActive(false);
                lineGeneral.SetActive(false);
            }

            // Các hàm hiển thị các panel trong màn hình cài đặt
            public void GamePanel()
            {
                DisablePanels();
                PanelGame.SetActive(true);
                lineGame.SetActive(true);
            }

            public void VideoPanel()
            {
                DisablePanels();
                PanelVideo.SetActive(true);
                lineVideo.SetActive(true);
            }

            public void ControlsPanel()
            {
                DisablePanels();
                PanelControls.SetActive(true);
                lineControls.SetActive(true);
            }

            public void MovementPanel()
            {
                DisablePanels();

                PanelMovement.SetActive(true);
                lineMovement.SetActive(true);
            }

            public void CombatPanel()
            {
                DisablePanels();
                PanelCombat.SetActive(true);
                lineCombat.SetActive(true);
            }

            public void GeneralPanel()
            {
                DisablePanels();
                PanelGeneral.SetActive(true);
                lineGeneral.SetActive(true);
            }

            // Các hàm phát âm thanh hiệu ứng
            public void PlayHover()
            {
                hoverSound.Play(); // Phát âm thanh hover
            }

            public void PlaySFXHover()
            {
                sliderSound.Play(); // Phát âm thanh slider
            }

            public void PlaySwoosh()
            {
                swooshSound.Play(); // Phát âm thanh chuyển cảnh
            }

            // Hiển thị cửa sổ xác nhận thoát game
            public void AreYouSure()
            {
                exitMenu.SetActive(true);
                if (extrasMenu) extrasMenu.SetActive(false);
                DisablePlayCampaign();
            }

            public void AreYouSureMobile()
            {
                exitMenu.SetActive(true);
                if (extrasMenu) extrasMenu.SetActive(false);
                mainMenu.SetActive(false);
                DisablePlayCampaign();
            }

            // Hiển thị menu Extras
            public void ExtrasMenu()
            {
                playMenu.SetActive(false);
                if (extrasMenu) extrasMenu.SetActive(true);
                exitMenu.SetActive(false);
            }

            // Thoát game
            public void QuitGame()
            {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Dừng game trong Unity Editor
                #else
                Application.Quit(); // Thoát game khi build ra
                #endif
            }

            // Hàm tải scene không đồng bộ với thanh loading
            IEnumerator LoadAsynchronously(string sceneName)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
                operation.allowSceneActivation = false;
                mainCanvas.SetActive(false);
                customCanvas.SetActive(false);

                loadingMenu.SetActive(true);

                while (!operation.isDone)
                {
                    float progress = Mathf.Clamp01(operation.progress / .95f);
                    loadingBar.value = progress;

                    // Chờ người dùng nhấn phím nếu waitForInput là true
                    if (operation.progress >= 0.9f && waitForInput)
                    {
                        loadingBar.value = 1;

                        if (Input.anyKeyDown)
                        {
                            operation.allowSceneActivation = true;
                        }
                    }
                    else if (operation.progress >= 0.9f && !waitForInput)
                    {
                        operation.allowSceneActivation = true;
                    }

                    yield return null;
                }
            }
        }
    }

}
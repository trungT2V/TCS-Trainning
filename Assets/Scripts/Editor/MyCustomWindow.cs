using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System;

public class MyCustomWindow : EditorWindow
{
    // Khai báo biến để chứa original prefab và material
    public GameObject originalPrefab;
    public Material originalMaterial;
    public GameObject originalPrefabUI;

    // Khai báo đường dẫn thư mục chứa ảnh
    public string folderPath = "Assets/Textures";

    // Tạo một cửa sổ từ menu
    [MenuItem("Window/Prefab Cloner")]
    public static void ShowWindow()
    {
        GetWindow<MyCustomWindow>("Prefab Cloner");
    }

    // Hiển thị giao diện người dùng
    private void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);

        originalPrefabUI = (GameObject)EditorGUILayout.ObjectField("Original UI Prefab", originalPrefabUI, typeof(GameObject), false);

        // Kéo thả prefab gốc
        originalPrefab = (GameObject)EditorGUILayout.ObjectField("Original Prefab", originalPrefab, typeof(GameObject), false);

        // Kéo thả material gốc
        originalMaterial = (Material)EditorGUILayout.ObjectField("Original Material", originalMaterial, typeof(Material), false);

        // Nhập đường dẫn thư mục
        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        // Button để clone prefab
        if (GUILayout.Button("Clone Prefab"))
        {
            ClonePrefabsWithTextures();
        }

        if (GUILayout.Button("Create UI"))
        {
            CreateUI();
        }

        if(GUILayout.Button("Create UI"))
        {
            SetMaterial();
        }
    }

    private void SetMaterial()
    {
        
    }

    private void CreateUI()
    {
        string[] imagePaths = Directory.GetFiles(folderPath, "*.png");

        foreach (string imagePath in imagePaths)
        {
            string fileName = Path.GetFileNameWithoutExtension(imagePath);
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            string spritePath = folderPath + "/UI/" + fileName + ".png";
            File.WriteAllBytes(spritePath, texture.EncodeToPNG());
            AssetDatabase.Refresh();

            GameObject newUI = Instantiate(originalPrefabUI);
            LabelElement element = newUI.GetComponent<LabelElement>();
            newUI.GetComponentInChildren<Image>().sprite = newSprite;
            element.labelType = Convert.ToInt32(fileName);
            string uiPrefabPath = folderPath + "/UIPrefabs" + "/" + fileName + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(newUI, uiPrefabPath);

            // Hủy đối tượng sau khi lưu
            DestroyImmediate(newUI);

            Debug.Log("Prefab created with texture: " + imagePath);
        }
    }

    // Hàm để nhân bản prefab với mỗi ảnh trong thư mục
    private void ClonePrefabsWithTextures()
    {
        // Kiểm tra xem originalPrefab và originalMaterial có được gán chưa
        if (originalPrefab == null || originalMaterial == null)
        {
            Debug.LogError("Original Prefab và Original Material cần được gán!");
            return;
        }

        // Kiểm tra đường dẫn thư mục có hợp lệ không
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("Đường dẫn thư mục không tồn tại: " + folderPath);
            return;
        }

        // Đường dẫn thư mục để lưu prefab
        string savePath = "Assets/Resources/Labels";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        // Lấy tất cả đường dẫn file ảnh trong thư mục
        string[] imagePaths = Directory.GetFiles(folderPath, "*.png");

        // Lặp qua tất cả ảnh và tạo prefab mới
        foreach (string imagePath in imagePaths)
        {
            // Tạo một material mới từ material gốc
            Material newMaterial = new Material(originalMaterial);

            // Load texture từ file ảnh
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            // Gán texture vào material mới
            newMaterial.SetTexture("_MainTex", texture);

            // Tạo một bản sao của prefab gốc
            GameObject newPrefabInstance = Instantiate(originalPrefab);

            // Gán material mới vào mesh renderer của prefab
            MeshRenderer renderer = newPrefabInstance.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = newMaterial;
            }

            string fileName = Path.GetFileNameWithoutExtension(imagePath);

            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            string spritePath = folderPath + "/UI/" + fileName + ".png";
            File.WriteAllBytes(spritePath, texture.EncodeToPNG());
            AssetDatabase.Refresh();


            GameObject newUI = Instantiate(originalPrefabUI);
            LabelElement element = newUI.GetComponent<LabelElement>();
            newUI.GetComponentInChildren<Image>().sprite = newSprite;
            element.labelType = Convert.ToInt32(fileName);
            string uiPrefabPath = folderPath + "/UIPrefabs" + "/" + fileName + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(newUI, uiPrefabPath);

            // save material
            string materialPath = folderPath + "/Materials" + "/" + fileName + ".mat";
            AssetDatabase.CreateAsset(newMaterial, materialPath);
            AssetDatabase.SaveAssets();

            string prefabPath = savePath + "/" + fileName + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(newPrefabInstance, prefabPath);

            // Hủy đối tượng sau khi lưu
            DestroyImmediate(newPrefabInstance);

            Debug.Log("Prefab created with texture: " + imagePath);
        }

        Debug.Log("Prefab cloning completed!");
    }
}

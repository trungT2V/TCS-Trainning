using UnityEngine;
using UnityEditor;
using System.IO;

public class MaterialTextureAssigner : EditorWindow
{
    public string folderPath = "Assets/Materials";  // Đường dẫn tới thư mục chứa materials

    [MenuItem("Window/Material Texture Assigner")]
    public static void ShowWindow()
    {
        GetWindow<MaterialTextureAssigner>("Material Texture Assigner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        if (GUILayout.Button("Assign Textures to Materials"))
        {
            AssignTexturesToMaterials();
        }
    }

    private void AssignTexturesToMaterials()
    {
        // Kiểm tra nếu thư mục tồn tại
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("Thư mục không tồn tại: " + folderPath);
            return;
        }

        // Lấy tất cả các file material trong thư mục
        string[] materialPaths = Directory.GetFiles(folderPath, "*.mat");

        foreach (string materialPath in materialPaths)
        {
            // Tải material
            Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
            if (material != null)
            {
                // Lấy tên material mà không có phần mở rộng
                string materialName = Path.GetFileNameWithoutExtension(materialPath);

                // Tìm texture có cùng tên trong thư mục
                string texturePath = Path.Combine(folderPath, materialName + ".png"); // Hoặc .jpg nếu cần

                if (File.Exists(texturePath))
                {
                    // Tải texture
                    Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
                    if (texture != null)
                    {
                        // Gán texture vào _MainTex
                        material.SetTexture("_MainTex", texture);
                        EditorUtility.SetDirty(material);  // Đánh dấu material là đã thay đổi
                        Debug.Log($"Đã gán texture '{texture.name}' cho material '{material.name}'");
                    }
                    else
                    {
                        Debug.LogError($"Không thể tải texture từ đường dẫn: {texturePath}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Không tìm thấy texture với tên '{materialName}' trong thư mục.");
                }
            }
            else
            {
                Debug.LogError($"Không thể tải material từ đường dẫn: {materialPath}");
            }
        }

        // Lưu tất cả các asset đã thay đổi
        AssetDatabase.SaveAssets();
        Debug.Log("Hoàn thành gán texture cho tất cả materials!");
    }
}

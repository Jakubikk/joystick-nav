using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.IO;

namespace QuestCameraKit.Menu.Editor
{
    /// <summary>
    /// Unity Editor utility to automatically set up the menu system UI.
    /// Run this from Unity Editor: Tools → Decart → Setup Menu UI
    /// </summary>
    public class MenuUISetup : EditorWindow
    {
        [MenuItem("Tools/Decart/Setup Menu UI")]
        public static void ShowWindow()
        {
            GetWindow<MenuUISetup>("Menu UI Setup");
        }

        private void OnGUI()
        {
            GUILayout.Label("Decart Quest 3 Menu UI Setup", EditorStyles.boldLabel);
            GUILayout.Space(10);

            GUILayout.Label("This will create all required UI elements for the menu system.", EditorStyles.wordWrappedLabel);
            GUILayout.Space(10);

            if (GUILayout.Button("Create Menu UI", GUILayout.Height(40)))
            {
                CreateMenuUI();
            }

            GUILayout.Space(20);
            GUILayout.Label("Instructions:", EditorStyles.boldLabel);
            GUILayout.Label("1. Make sure DecartAI-Main scene is open", EditorStyles.wordWrappedLabel);
            GUILayout.Label("2. Click 'Create Menu UI' button above", EditorStyles.wordWrappedLabel);
            GUILayout.Label("3. UI will be added to the scene", EditorStyles.wordWrappedLabel);
            GUILayout.Label("4. Connect references in MenuManager", EditorStyles.wordWrappedLabel);
        }

        private static void CreateMenuUI()
        {
            // Create main canvas if it doesn't exist
            Canvas mainCanvas = FindObjectOfType<Canvas>();
            if (mainCanvas == null)
            {
                GameObject canvasObj = new GameObject("MenuCanvas");
                mainCanvas = canvasObj.AddComponent<Canvas>();
                mainCanvas.renderMode = RenderMode.WorldSpace;
                
                CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
                scaler.dynamicPixelsPerUnit = 10;
                
                canvasObj.AddComponent<GraphicRaycaster>();
                
                RectTransform canvasRect = mainCanvas.GetComponent<RectTransform>();
                canvasRect.sizeDelta = new Vector2(1920, 1080);
                canvasRect.localPosition = new Vector3(0, 1.5f, 2f);
                canvasRect.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            }

            // Create Menu Panel
            GameObject menuPanel = CreatePanel("MenuPanel", mainCanvas.transform);
            
            // Create Title Text
            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(menuPanel.transform, false);
            TMP_Text titleText = titleObj.AddComponent<TextMeshProUGUI>();
            titleText.text = "Decart Quest 3";
            titleText.fontSize = 72;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            
            RectTransform titleRect = titleObj.GetComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0, 0.9f);
            titleRect.anchorMax = new Vector2(1, 1);
            titleRect.offsetMin = new Vector2(50, 0);
            titleRect.offsetMax = new Vector2(-50, -20);

            // Create Menu Items Container
            GameObject containerObj = new GameObject("MenuItemsContainer");
            containerObj.transform.SetParent(menuPanel.transform, false);
            
            VerticalLayoutGroup layout = containerObj.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 20;
            layout.padding = new RectOffset(50, 50, 20, 20);
            layout.childControlHeight = false;
            layout.childControlWidth = true;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = true;
            
            RectTransform containerRect = containerObj.GetComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0, 0.2f);
            containerRect.anchorMax = new Vector2(1, 0.85f);
            containerRect.offsetMin = Vector2.zero;
            containerRect.offsetMax = Vector2.zero;

            // Create Menu Item Prefab
            GameObject menuItemPrefab = CreateMenuItemPrefab();
            
            // Save prefab
            string prefabPath = "Assets/Samples/DecartAI-Quest/Prefabs";
            if (!Directory.Exists(prefabPath))
            {
                Directory.CreateDirectory(prefabPath);
            }
            
            PrefabUtility.SaveAsPrefabAsset(menuItemPrefab, $"{prefabPath}/MenuItem.prefab");
            
            // Create feature panels
            CreateFeaturePanel("TimeTravelPanel", mainCanvas.transform);
            CreateFeaturePanel("VirtualTryOnPanel", mainCanvas.transform);
            CreateFeaturePanel("BiomeTransformPanel", mainCanvas.transform);
            CreateFeaturePanel("VideoGameStylePanel", mainCanvas.transform);
            CreateFeaturePanel("CustomPromptPanel", mainCanvas.transform);

            // Save list item prefab
            GameObject listItemPrefab = CreateListItemPrefab();
            PrefabUtility.SaveAsPrefabAsset(listItemPrefab, $"{prefabPath}/ListItem.prefab");
            
            Debug.Log("Menu UI created successfully! Please connect references in MenuManager component.");
            
            EditorUtility.DisplayDialog("Success", "Menu UI has been created!\n\nNext steps:\n1. Add MenuManager component to scene\n2. Connect UI references in Inspector\n3. Add feature components\n4. Test in Unity Editor", "OK");
        }

        private static GameObject CreatePanel(string name, Transform parent)
        {
            GameObject panel = new GameObject(name);
            panel.transform.SetParent(parent, false);
            
            Image image = panel.AddComponent<Image>();
            image.color = new Color(0.1f, 0.1f, 0.15f, 0.95f);
            
            RectTransform rect = panel.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(100, 100);
            rect.offsetMax = new Vector2(-100, -100);
            
            return panel;
        }

        private static GameObject CreateMenuItemPrefab()
        {
            GameObject item = new GameObject("MenuItem");
            
            Image bg = item.AddComponent<Image>();
            bg.color = new Color(1, 1, 1, 0.6f);
            
            RectTransform itemRect = item.GetComponent<RectTransform>();
            itemRect.sizeDelta = new Vector2(800, 80);
            
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(item.transform, false);
            
            TMP_Text text = textObj.AddComponent<TextMeshProUGUI>();
            text.text = "Menu Item";
            text.fontSize = 48;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.black;
            
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(20, 10);
            textRect.offsetMax = new Vector2(-20, -10);
            
            return item;
        }

        private static GameObject CreateListItemPrefab()
        {
            GameObject item = new GameObject("ListItem");
            
            Image bg = item.AddComponent<Image>();
            bg.color = new Color(1, 1, 1, 0.6f);
            
            Button button = item.AddComponent<Button>();
            
            RectTransform itemRect = item.GetComponent<RectTransform>();
            itemRect.sizeDelta = new Vector2(700, 60);
            
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(item.transform, false);
            
            TMP_Text text = textObj.AddComponent<TextMeshProUGUI>();
            text.text = "List Item";
            text.fontSize = 36;
            text.alignment = TextAlignmentOptions.Left;
            text.color = Color.black;
            
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(20, 5);
            textRect.offsetMax = new Vector2(-20, -5);
            
            return item;
        }

        private static void CreateFeaturePanel(string name, Transform parent)
        {
            GameObject panel = CreatePanel(name, parent);
            panel.SetActive(false);
            
            // Add title
            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(panel.transform, false);
            TMP_Text titleText = titleObj.AddComponent<TextMeshProUGUI>();
            titleText.text = name.Replace("Panel", "");
            titleText.fontSize = 60;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = Color.white;
            
            RectTransform titleRect = titleObj.GetComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0, 0.9f);
            titleRect.anchorMax = new Vector2(1, 1);
            titleRect.offsetMin = new Vector2(50, 0);
            titleRect.offsetMax = new Vector2(-50, -10);

            // Add instructions
            GameObject instrObj = new GameObject("InstructionsText");
            instrObj.transform.SetParent(panel.transform, false);
            TMP_Text instrText = instrObj.AddComponent<TextMeshProUGUI>();
            instrText.text = "Instructions will appear here";
            instrText.fontSize = 32;
            instrText.alignment = TextAlignmentOptions.Center;
            instrText.color = Color.gray;
            
            RectTransform instrRect = instrObj.GetComponent<RectTransform>();
            instrRect.anchorMin = new Vector2(0, 0.8f);
            instrRect.anchorMax = new Vector2(1, 0.88f);
            instrRect.offsetMin = new Vector2(50, 0);
            instrRect.offsetMax = new Vector2(-50, 0);

            // Add content area
            GameObject contentObj = new GameObject("ContentArea");
            contentObj.transform.SetParent(panel.transform, false);
            
            ScrollRect scroll = contentObj.AddComponent<ScrollRect>();
            scroll.vertical = true;
            scroll.horizontal = false;
            
            RectTransform contentRect = contentObj.GetComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0.1f, 0.1f);
            contentRect.anchorMax = new Vector2(0.9f, 0.75f);
            contentRect.offsetMin = Vector2.zero;
            contentRect.offsetMax = Vector2.zero;
            
            // Add viewport
            GameObject viewportObj = new GameObject("Viewport");
            viewportObj.transform.SetParent(contentObj.transform, false);
            
            RectTransform viewportRect = viewportObj.GetComponent<RectTransform>();
            viewportRect.anchorMin = Vector2.zero;
            viewportRect.anchorMax = Vector2.one;
            viewportRect.offsetMin = Vector2.zero;
            viewportRect.offsetMax = Vector2.zero;
            
            viewportObj.AddComponent<Image>();
            viewportObj.AddComponent<Mask>().showMaskGraphic = false;
            
            scroll.viewport = viewportRect;
            
            // Add content container
            GameObject container = new GameObject("Container");
            container.transform.SetParent(viewportObj.transform, false);
            
            VerticalLayoutGroup layout = container.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(10, 10, 10, 10);
            
            ContentSizeFitter fitter = container.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            
            RectTransform containerRect = container.GetComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0, 1);
            containerRect.anchorMax = Vector2.one;
            containerRect.pivot = new Vector2(0.5f, 1);
            
            scroll.content = containerRect;
        }
    }
}

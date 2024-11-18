using UnityEngine;
using UnityEngine.UI; // Для работы с UI

public class DocumentUI : MonoBehaviour
{
    public static DocumentUI Instance;
    public GameObject documentPanel; // Панель UI для документа
    public Text documentText; // Поле текста
    public Image documentImage; // Компонент Image для картинки
    public MonoBehaviour cameraControlScript; // Скрипт управления камерой
    public float maxImageWidth = 500f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        documentPanel.SetActive(false); // Скрываем панель при старте
        documentImage.gameObject.SetActive(false); // Скрываем картинку при старте
    }

    // Метод для отображения документа с картинкой
    public void ShowDocument(string text, Sprite image = null)
    {
        documentText.text = text;
        documentPanel.SetActive(true);

        // Если картинка передана, показываем её
        if (image != null)
        {
            documentImage.sprite = image;
            documentImage.gameObject.SetActive(true); // Включаем изображение

            // Сохраняем размеры картинки и подстраиваем размер UI элемента
            SetImageSize(image);
        }
        else
        {
            documentImage.gameObject.SetActive(false); // Если изображения нет, скрываем его
        }

        // Включаем курсор и отключаем управление камерой
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (cameraControlScript != null)
        {
            cameraControlScript.enabled = false;
        }
    }

    // Метод для установки размера изображения с сохранением пропорций
    private void SetImageSize(Sprite image)
    {
        // Получаем исходные размеры картинки
        float imageWidth = image.rect.width;
        float imageHeight = image.rect.height;

        // Настройка размера компонента Image с сохранением пропорций
        RectTransform rt = documentImage.GetComponent<RectTransform>();
        
        // Сохраняем пропорции изображения, подстраиваем под размеры канваса
        float aspectRatio = imageWidth / imageHeight;

        // Настроим максимальную ширину, по которой будет регулироваться высота
        float maxWidth = maxImageWidth;

        if (imageWidth > maxWidth)
        {
            float scaleFactor = maxWidth / imageWidth;
            rt.sizeDelta = new Vector2(maxWidth, imageHeight * scaleFactor);
        }
        else
        {
            rt.sizeDelta = new Vector2(imageWidth, imageHeight);
        }
    }

    // Метод для закрытия документа
    public void CloseDocument()
    {
        documentPanel.SetActive(false);

        // Скрываем курсор и включаем управление камерой
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (cameraControlScript != null)
        {
            cameraControlScript.enabled = true;
        }
    }
}

/*
README
Накидывать на документы которые должны открываться.
Также на объект документа поставить слой - Document.
В documentInfo вписать необходимый для отображения текст.
В documentImage кинуть спрайтовую картинку, если не кинуть картинки не будет(логично)
*/
using UnityEngine;

public class DocumentInteraction : MonoBehaviour
{
    public string documentInfo; // Информация о документе
    public Sprite documentImage; // Картинка для документа

    // Открытие документа
    public void OpenDocument()
    {
        Debug.Log($"Чтение документа: {documentInfo}");
        DocumentUI.Instance.ShowDocument(documentInfo, documentImage);
    }
}
/*RinKirsan*/
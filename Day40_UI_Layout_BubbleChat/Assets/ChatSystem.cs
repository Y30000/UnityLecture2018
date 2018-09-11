using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour {
    public RectTransform chatPrefab;
    public RectTransform content;
    public InputField inputField;
	// Use this for initialization
	void Start () {
        inputField.onEndEdit.AddListener((message) =>
        {
            AddMessage(message, Random.Range(0, 2) == 0 ? true : false);
            inputField.Select();
            inputField.ActivateInputField();
            //inputField.text = string.Empty;
        });
	}
	
    public void AddMessage(string message, bool isAlignLeft)
    {
        if(message.Length > 0)
        {
            RectTransform chat = Instantiate(chatPrefab, content);
            chat.GetComponent<ChatMessage>().SetMessage(message, isAlignLeft);
        }
    }
}

using ReHackChatHacking;
using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using TwitchChatConnect.Config;
using TwitchChatConnect.Data;
using UnityEngine;

public class ListenToTwitchChatMono : MonoBehaviour
{
    public TwitchChatAccessDataFromFile m_directPasswordFilePath;
    public TwitchChatAccessData m_info;
    public TwitchChatClient m_clientToStart;
    public TwitchConnectConfig m_config;

    public bool m_autoStart=true;

    private void Start()
    {
        if (m_autoStart)
        {
            Init();
        }
    }

    public void SetWith(TwitchConnectConfig config) {
        m_config = config;
    }

    private void Init()
    {
        
        m_clientToStart.Init(m_config, () =>
        {
            m_clientToStart.onChatMessageReceived += OnChatMessageReceived;
            m_clientToStart.onChatCommandReceived += OnChatMessageReceived;
        },
            message =>
            {
                // Error when initializing.
                Debug.LogError(message);
            });
    }

    private void OnChatMessageReceived(TwitchChatMessage chatMessage)
    {
        IReHackChatMessageGet message = new ReHackChatMessage(chatMessage.User.DisplayName, chatMessage.Message, ChatPlatform.Twitch);
        m_messageReceived.Invoke(message);
    }
    public IRehackMessageUnityEvent m_messageReceived;

}
using ReHackChatHacking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RehackMessageRelayByGroupMono : MonoBehaviour
{
    public AccountRehackMessageRelay m_relay;

}


[System.Serializable]
public class AccountRehackMessageRelay
{
    public RehackMessageRelayByGroup m_relayAllPlatform;
    public RehackMessageRelayByGroupOnPlatforms m_relayMultiPlatform;
    public IRehackMessageUnityEvent m_relay;
    public IRehackMessageUnityEvent m_notRelay;

    public void Push(IReHackChatMessageGet message)
    {
        m_relayAllPlatform.Push(message, out bool pushed);
        if (!pushed)
            m_relayAllPlatform.Push(message, out pushed);
        if (!pushed)
            m_notRelay.Invoke(message);
        if (pushed)
            m_relay.Invoke(message);
    }

    public void AddAccountPlatformLess(in string accountName) {
        m_relayAllPlatform.m_authorizedAccounts.m_accounts.Add(new ReHackUserId(accountName));
    }
    public void AddAccountPlatformDependant(in string accountName, string platform)
    {

        m_relayAllPlatform.m_authorizedAccounts.m_accounts.Add(new ReHackUserIdOnPlatform(accountName, platform.ToLower()));
    }
        public void AddAccountPlatformDependant(in string accountName, ChatPlatform platform) {

            m_relayAllPlatform.m_authorizedAccounts.m_accounts.Add(new ReHackUserIdOnPlatform(accountName, platform.ToString().ToLower()));
        }

}



[System.Serializable]
public class RehackMessageRelayByGroup
{
    public ListOfAccountId m_authorizedAccounts = new ListOfAccountId();
    public IRehackMessageUnityEvent m_relayAuthorized = new IRehackMessageUnityEvent();
    
    public void Push(IReHackChatMessageGet message, out bool wasPushed)
    {
        wasPushed = false;
        Contains(message, out wasPushed);
        m_relayAuthorized.Invoke(message);
    }

    public void Contains(IReHackChatMessageGet message, out bool hasIt)
    {
        message.GetUserName(out string name);
        Contains(in name,  out hasIt);
    }
    public void Contains(in string userName,  out bool hasIt)
    {
        m_authorizedAccounts.Contains(in userName,  out hasIt);
    }
}
[System.Serializable]
public class RehackMessageRelayByGroupOnPlatforms
{

    public ListOfAccountIdOnPlatform m_authorizedAccounts = new ListOfAccountIdOnPlatform();
    public IRehackMessageUnityEvent m_relayAuthorized = new IRehackMessageUnityEvent();
    public void Push(IReHackChatMessageGet message, out bool wasPushed)
    {
        wasPushed = false;
        Contains(message, out wasPushed);
        m_relayAuthorized.Invoke(message);
    }

    public void Contains(IReHackChatMessageGet message, out bool hasIt)
    {
        message.GetUserName(out string name);
        message.GetPlatfomUsed(out string platform);
        Contains(in name, in platform, out hasIt);
    }
    public void Contains(in string userName,in string platformName, out bool hasIt)
    {

        m_authorizedAccounts.Contains(in userName, in platformName, out hasIt);
    }
}

[System.Serializable]
public class ListOfAccountId
{
    public List<ReHackUserId> m_accounts = new List<ReHackUserId>();
    public void Contains(in string userName, out bool hasIt)
    {
        for (int i = 0; i < m_accounts.Count; i++)
        {
            if (
                m_accounts[i].m_userNameId.Length == userName.Length &&
                m_accounts[i].m_userNameId.ToLower() == userName.ToLower()
                )
            {
                hasIt = true;
                return;
            }
        }
        hasIt = false;
    }
}
[System.Serializable]
public class ListOfAccountIdOnPlatform
{
    public List<ReHackUserIdOnPlatform> m_accounts = new List<ReHackUserIdOnPlatform>();
    public void Contains(in string userName, in string platformName, out bool hasIt)
    {
        for (int i = 0; i < m_accounts.Count; i++)
        {
            if (
                m_accounts[i].m_userNameId.Length == userName.Length &&
                m_accounts[i].m_platformName.Length == platformName.Length &&
                m_accounts[i].m_userNameId.ToLower() == userName.ToLower() &&
                m_accounts[i].m_platformName.ToLower() == platformName.ToLower()
                )
            {
                hasIt = true;
                return;
            }
        }
        hasIt = false;
    }
}

[System.Serializable]
public class ReHackUserId
{
    public string m_userNameId="";

    public ReHackUserId(string userNameId)
    {
        m_userNameId = userNameId;
    }
    public ReHackUserId()
    {
        m_userNameId = "";
    }
}
[System.Serializable]
public class ReHackUserIdOnPlatform : ReHackUserId
{
    public string m_platformName="";

    public ReHackUserIdOnPlatform(string userNameId, string platformName) : base(userNameId)
    {
        m_platformName = platformName;
    }
    public ReHackUserIdOnPlatform() : base("")
    {
        m_platformName = "";
    }
}

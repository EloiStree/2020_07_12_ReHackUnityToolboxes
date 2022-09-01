using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountTagRegisterMono : MonoBehaviour
{
    public AccountTagRegister m_register;

}
[System.Serializable]
public class AccountTagRegister
{
    public List<AccountTagAccountGroup> m_tagRegister = new List<AccountTagAccountGroup>();

    public void Contains(in string tag, in string accountNameId, out bool exists)
    {
        Get(tag, out bool found, out AccountTagAccountGroup group);
        if (found){
            group.m_accountPlatfomLess.Contains(in accountNameId, out exists);
            return;
        }
        exists = false;
    }
    public void Contains(in string tag, in string accountNameId, in string accountPlatform, out bool exists)
    {
        Get(tag, out bool found, out AccountTagAccountGroup group);
        if (found) {
            group.m_accountPlatfomSensitive.Contains(in accountNameId, in accountPlatform, out exists);
            return;
        }
        exists = false;
    }

    public void Get(in string tag, out bool found, out AccountTagAccountGroup group) {
        for (int i = 0; i < m_tagRegister.Count; i++)
        {
            if (m_tagRegister[i].m_tag.m_tagName == tag) {
                found = true;
                group = m_tagRegister[i];
                return;
            }
        }
        found = false;
        group = null;

    }

}
[System.Serializable]
public class AccountTagAccountGroup
{
    public AccountTag m_tag;
    public ListOfAccountId m_accountPlatfomLess;
    public ListOfAccountIdOnPlatform m_accountPlatfomSensitive;

}
[System.Serializable]
public class AccountTag
{
    public string m_tagName;

}

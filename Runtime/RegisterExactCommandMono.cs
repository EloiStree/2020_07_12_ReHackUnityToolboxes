


using System.Collections.Generic;
using UnityEngine;

public class RegisterExactCommandMono : MonoBehaviour{

    public CommandsExecutableByAll m_commandsAll;
    public ShortcutExecutableByAll m_shortcutAll;
    public CommandsExecutableBySome m_commandsAuthorized;
    public ShortcutExecutableBySome m_shortcutAuthorized;


}


[System.Serializable]
public class ChatCommandLineExact
{
    public string m_chatFormat;
    public string[] m_toSendCommandLine;
}
[System.Serializable]
public class ChatShortcutExact
{
    public string m_chatFormat;
    public string[] m_toSendCommandLine;
}

[System.Serializable]
public class CommandsExecutableByAll
{
    public List<ChatCommandLineExact> m_commands;
}
[System.Serializable]
public class CommandsExecutableBySome
{
    public AuthorizedUsers m_authorized;
    public List<ChatCommandLineExact> m_commands;
}
[System.Serializable]
public class ShortcutExecutableByAll
{
    public List<ChatShortcutExact> m_commands;
}
[System.Serializable]
public class ShortcutExecutableBySome
{
    public AuthorizedUsers m_authorized;
    public List<ChatShortcutExact> m_commands;
}

[System.Serializable]
public class AuthorizedUsers
{
    public List<AuthorizedUser> m_users = new List<AuthorizedUser>();
}
[System.Serializable]
public class AuthorizedUser
{
    public string m_platformName;
    public string m_accountName;

}
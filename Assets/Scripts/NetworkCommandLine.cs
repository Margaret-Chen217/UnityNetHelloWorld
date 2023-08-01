using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkCommandLine : MonoBehaviour
{
    private NetworkManager networkManager;
    void Start()
    {   
        networkManager = GetComponentInParent<NetworkManager>();
        if (Application.isEditor)
        {
            return;
        }

        var args = GetCommandlineArgs();

        if (args.TryGetValue("-mode", out string mode))
        {
            switch (mode)
            {
                case "server":
                    networkManager.StartServer();
                    break;
                case "host":
                    networkManager.StartHost();
                    break;
                case "client":
                    networkManager.StartServer();
                    break;
            }
        }
    }

    /// <summary>
    /// 获取命令行参数
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string> GetCommandlineArgs()
    {
        //创建一个Dic存储命令行参数
        Dictionary<string, string> argDictionary = new Dictionary<string, string>();
        var args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i].ToLower();
            if (arg.StartsWith("-"))
            {   
                var value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
                value = (value?.StartsWith("-") ?? false) ? null : value;
                
                argDictionary.Add(arg, value);
            }
        }
        return argDictionary;
    }
}

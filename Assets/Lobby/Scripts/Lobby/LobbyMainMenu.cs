using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Prototype.NetworkLobby
{
	//Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
	public class LobbyMainMenu : MonoBehaviour
	{
		public LobbyManager lobbyManager;

		public RectTransform lobbyServerList;
		public RectTransform lobbyPanel;

		public InputField ipInput;
		public InputField matchNameInput;
		public Text ipText;

		public void OnEnable()
		{
			lobbyManager.topPanel.ToggleVisibility(true);

			ipInput.onEndEdit.RemoveAllListeners();
			ipInput.onEndEdit.AddListener(onEndEditIP);

			matchNameInput.onEndEdit.RemoveAllListeners();
			matchNameInput.onEndEdit.AddListener(onEndEditGameName);
			ipText.text = GetLocalIP();
		}

		public void OnClickHost()
		{
			lobbyManager.StartHost();
		}

		public void OnClickJoin()
		{
			lobbyManager.ChangeTo(lobbyPanel);

			lobbyManager.networkAddress = ipInput.text;
			lobbyManager.StartClient();

			lobbyManager.backDelegate = lobbyManager.StopClientClbk;
			lobbyManager.DisplayIsConnecting();

			lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
		}

		public void OnClickDedicated()
		{
			lobbyManager.ChangeTo(null);
			lobbyManager.StartServer();

			lobbyManager.backDelegate = lobbyManager.StopServerClbk;

			lobbyManager.SetServerInfo("Dedicated Server", lobbyManager.networkAddress);
		}

		public void OnClickCreateMatchmakingGame()
		{
			lobbyManager.StartMatchMaker();
			lobbyManager.matchMaker.CreateMatch(
				matchNameInput.text,
				(uint)lobbyManager.maxPlayers,
				true,
				"", "", "", 0, 0,
				lobbyManager.OnMatchCreate);

			lobbyManager.backDelegate = lobbyManager.StopHost;
			lobbyManager._isMatchmaking = true;
			lobbyManager.DisplayIsConnecting();

			lobbyManager.SetServerInfo("Matchmaker Host", lobbyManager.matchHost);
		}

		public void OnClickOpenServerList()
		{
			lobbyManager.StartMatchMaker();
			lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
			lobbyManager.ChangeTo(lobbyServerList);
		}

		void onEndEditIP(string text)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				OnClickJoin();
			}
		}

		void onEndEditGameName(string text)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				OnClickCreateMatchmakingGame();
			}
		}

		public static string GetLocalIP()
		{
			//IPHostEntry host;
			//string localIP = "0.0.0.0";
			//host = Dns.GetHostEntry(Dns.GetHostName());
			//foreach (IPAddress ip in host.AddressList)
			//{
			//    if (ip.AddressFamily == AddressFamily.InterNetwork)
			//    {
			//        localIP = ip.ToString();
			//        break;
			//    }
			//}
			//return localIP;
			string output = "";

			foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
			{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
				NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
				NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

				if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif
				{
					foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
					{
						
							if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
							{
								output = ip.Address.ToString();
							}
						}

					}

				}
			return output;
			}
		}
	}

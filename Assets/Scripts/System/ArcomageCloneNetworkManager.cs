using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.Events;

namespace ArcomageClone
{
    public class ArcomageCloneNetworkManager : SingletonMB<ArcomageCloneNetworkManager>
    {
        public UnityEvent OnCreateGame;
        public UnityEvent OnJoinGame;

        private UnityTransport _transport;

        protected override async void Awake()
        {
            base.Awake();

            await Authenticate();
        }

        private static async Task Authenticate()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        private void Start()
        {
            _transport = GetComponent<UnityTransport>();
        }

        public async void CreateGame()
        {
            Allocation alloc = await RelayService.Instance.CreateAllocationAsync(2);
            Debug.Log(await RelayService.Instance.GetJoinCodeAsync(alloc.AllocationId)); //join code

            _transport.SetHostRelayData(alloc.RelayServer.IpV4, (ushort)alloc.RelayServer.Port, alloc.AllocationIdBytes, alloc.Key, alloc.ConnectionData);

            NetworkManager.Singleton.StartHost();
        }

        public async void JoinGame()
        {
            JoinAllocation alloc = await RelayService.Instance.JoinAllocationAsync("");

            _transport.SetClientRelayData(alloc.RelayServer.IpV4, (ushort)alloc.RelayServer.Port, alloc.AllocationIdBytes, alloc.Key, alloc.ConnectionData, alloc.HostConnectionData);

            NetworkManager.Singleton.StartClient();
        }
    }
}
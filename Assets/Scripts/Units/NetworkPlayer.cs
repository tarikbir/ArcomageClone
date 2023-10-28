using Unity.Netcode;

namespace ArcomageClone.Units
{
    public class NetworkPlayer : NetworkBehaviour
    {
        private NetworkVariable<int> _builderGain;
        private NetworkVariable<int> _builder;
        private NetworkVariable<int> _mightGain;
        private NetworkVariable<int> _might;
        private NetworkVariable<int> _magicGain;
        private NetworkVariable<int> _magic;
    }
}
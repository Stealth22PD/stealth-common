using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Common.Natives
{
    public static class Functions
    {
        public static object CallByName(string nativeName, System.Type returnType, params Rage.Native.NativeArgument[] arguments)
        {
            return Rage.Native.NativeFunction.CallByName(nativeName, returnType, arguments);
        }

        public static void CallByName(string nativeName, params Rage.Native.NativeArgument[] arguments)
        {
            Rage.Native.NativeFunction.CallByName<uint>(nativeName, arguments);
        }

        public static object CallByHash(ulong hash, System.Type returnType, params Rage.Native.NativeArgument[] arguments)
        {
            return Rage.Native.NativeFunction.CallByHash(hash, returnType, arguments);
        }

        public static void CallByHash(ulong hash, params Rage.Native.NativeArgument[] arguments)
        {
            Rage.Native.NativeFunction.CallByHash<uint>(hash, arguments);
        }
    }
}

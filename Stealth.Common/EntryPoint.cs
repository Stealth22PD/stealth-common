using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Rage.Attributes.Plugin("Stealth.Common", Description = "Support module for Stealth22's plugins", Author = "Stealth22")]
namespace Stealth.Common
{
    public static class EntryPoint
    {
        public static Random gRandom = new Random();

        public static void Main()
        {
            GameFiber.Yield();
        }
    }
}
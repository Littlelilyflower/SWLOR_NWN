﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.Scripting;
using SWLOR.Game.Server.Extension;
using SWLOR.Game.Server.Messaging;

namespace NWN
{
    public class Entrypoints
    {
        public delegate void ScriptDelegate(uint oidSelf);
        public const int SCRIPT_HANDLED = 0;
        public const int SCRIPT_NOT_HANDLED = -1;
        private static Dictionary<string, ScriptDelegate> _scripts = new Dictionary<string, ScriptDelegate>();
        
        // This is called once every main loop frame, outside of object context
        public static void OnMainLoop(ulong frame)
        {
        }

        //
        // This is called every time a named script is scheduled to run.
        // oidSelf is the object running the script (OBJECT_SELF), and script
        // is the name given to the event handler (e.g. via SetEventScript).
        // If the script is not handled in the managed code, and needs to be
        // forwarded to the original NWScript VM, return SCRIPT_NOT_HANDLED.
        // Otherwise, return either 0/SCRIPT_HANDLED for void main() scripts,
        // or an int (0 or 1) for StartingConditionals
        //
        public static int OnRunScript(string script, uint oidSelf)
        {
            try
            {
                Console.WriteLine("Script = " + script);
                if (_scripts.ContainsKey(script))
                {
                    _scripts[script].Invoke(oidSelf);
                    //Console.WriteLine("Invoking: " + script + ", type = " + instance.GetType());
                    //var method = instance.GetType().GetMethod("Main", BindingFlags.Public | BindingFlags.Instance);
                    //if(method == null)
                    //    throw new Exception("Could not locate Main method on script instance: " + script);

                    //method.Invoke(_scripts[script], null);
                    return SCRIPT_HANDLED;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToMessageAndCompleteStacktrace());
                return SCRIPT_NOT_HANDLED;
            }
            return SCRIPT_NOT_HANDLED;
        }

        //
        // This is called once when the internal structures have been initialized
        // The module is not yet loaded, so most NWScript functions will fail if
        // called here.
        //
        public static void OnStart()
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.Namespace == "NWN.Scripts");

            foreach (var type in types)
            {
                var name = type.Name;
                var method = type.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);
                if (method == null)
                {
                    Console.WriteLine("Skipping: " + name);
                    continue;
                }

                if (method.GetParameters().Length != 1)
                    continue;

                if (method != null)
                {
                    _scripts[name] = (ScriptDelegate) Delegate.CreateDelegate(typeof(ScriptDelegate), method);
                    Console.WriteLine("Registered script: " + name);
                }
            }
        }
    }
}
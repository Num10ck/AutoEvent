﻿// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         AutoEvent
//    Project:          AutoEvent
//    FileName:         ReloadConfigs.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/13/2023 4:29 PM
//    Created Date:     09/13/2023 4:29 PM
// -----------------------------------------

using System;
using System.Collections.Generic;
using AutoEvent.Interfaces;
using CommandSystem;
using Exiled.API.Features;

namespace AutoEvent.Commands.Debug;


public class AntiEnd : ICommand, IUsageProvider
{
    public string Command => nameof(AntiEnd);
    public string[] Aliases => Array.Empty<string>();
    public string Description => "Prevents an event from ending.";
    public string[] Usage => new string[] { "Enable / Disable / [Toggle]" };

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {

#if EXILED
        if (!((CommandSender)sender).CheckPermission("ev.debug"))
        {
            response = "You do not have permission to use this command";
            return false;
        }
#else
        var config = AutoEvent.Singleton.Config;
        var player = Player.Get(sender);
        if (sender is ServerConsoleSender || sender is CommandSender cmdSender && cmdSender.FullPermissions)
        {
            goto skipPermissionCheck;
        }

        if (!config.PermissionList.Contains(ServerStatic.PermissionsHandler._members[player.UserId]))
        {
            response = "<color=red>You do not have permission to use this command!</color>";
            return false;
        }
#endif

        skipPermissionCheck:

        if (arguments.Count >= 1 && arguments.At(0).ToLower() == "enable")
        {
            DebugLogger.AntiEnd = true;
        }
        else if (arguments.Count >= 1 && arguments.At(0).ToLower() == "disable")
        {
            DebugLogger.AntiEnd = false;
        }
        else
        {
            DebugLogger.AntiEnd = !DebugLogger.AntiEnd;
        }
        response = $"Anti-End {(DebugLogger.AntiEnd ? "Enabled" : "Disabled")}.";
        return true;
    }
}
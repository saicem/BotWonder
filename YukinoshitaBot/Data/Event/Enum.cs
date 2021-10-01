﻿// <copyright file="Enum.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum MessageType
    {
        TextMessage,
        PictureMessage
    }

    /// <summary>
    /// 发消息者类型枚举
    /// </summary>
    public enum SenderType
    {
        Friend = 1,
        Group = 2,
        TempSession = 3
    }
}

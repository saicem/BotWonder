// <copyright file="YukinoshitaController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using YukinoshitaBot.Data.Attributes;
    using YukinoshitaBot.Data.Controller;
    using YukinoshitaBot.Data.Event;
    using YukinoshitaBot.Data.OpqApi;
    using YukinoshitaBot.Extensions;

    /// <summary>
    /// 实现控制器
    /// </summary>
    internal class YukinoshitaController : IMessageHandler
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ControllerCollection controllers;

        /// <summary>
        /// Initializes a new instance of the <see cref="YukinoshitaController"/> class.
        /// </summary>
        /// <param name="serviceProvider">服务容器</param>
        /// <param name="controllerCollection">控制器容器</param>
        public YukinoshitaController(
            IServiceProvider serviceProvider,
            ControllerCollection controllerCollection)
        {
            this.serviceProvider = serviceProvider;
            this.controllers = controllerCollection;
        }

        /// <inheritdoc/>
        public void OnFriendPictureMsgRecieved(PictureMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!CheckMatch(msg.Content, controller.ControllerAttribute.Command, controller.ControllerAttribute.MatchMethod))
                {
                    continue;
                }

                if (controller.FriendImageHandlers.Any())
                {
                    foreach (var method in controller.FriendImageHandlers)
                    {
                        using var scope = this.serviceProvider.CreateScope();
                        var controllerObj = GetController(controller.ControllerType, scope);
                        method.Invoke(controllerObj, new object[] { msg });
                    }

                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnFriendTextMsgRecieved(TextMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!CheckMatch(msg.Content, controller.ControllerAttribute.Command, controller.ControllerAttribute.MatchMethod))
                {
                    continue;
                }

                if (controller.FriendTextHandlers.Any())
                {
                    foreach (var method in controller.FriendTextHandlers)
                    {
                        using var scope = this.serviceProvider.CreateScope();
                        var controllerObj = GetController(controller.ControllerType, scope);
                        method.Invoke(controllerObj, new object[] { msg });
                    }

                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnGroupPictureMsgRecieved(PictureMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!CheckMatch(msg.Content, controller.ControllerAttribute.Command, controller.ControllerAttribute.MatchMethod))
                {
                    continue;
                }

                if (controller.GroupImageHandlers.Any())
                {
                    foreach (var method in controller.GroupImageHandlers)
                    {
                        using var scope = this.serviceProvider.CreateScope();
                        var controllerObj = GetController(controller.ControllerType, scope);
                        method.Invoke(controllerObj, new object[] { msg });
                    }

                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnGroupTextMsgRecieved(TextMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!CheckMatch(msg.Content, controller.ControllerAttribute.Command, controller.ControllerAttribute.MatchMethod))
                {
                    continue;
                }

                if (controller.GroupTextHandlers.Any())
                {
                    foreach (var method in controller.GroupTextHandlers)
                    {
                        using var scope = this.serviceProvider.CreateScope();
                        var controllerObj = GetController(controller.ControllerType, scope);
                        method.Invoke(controllerObj, new object[] { msg });
                    }

                    break;
                }
            }
        }

        private static bool CheckMatch(string msg, string cmd, CommandMatchMethod method) => method switch
        {
            CommandMatchMethod.Strict => msg == cmd,
            CommandMatchMethod.StartWith => msg.StartsWith(cmd),
            CommandMatchMethod.Regex => Regex.IsMatch(msg, cmd),
            _ => false
        };

        private static BotControllerBase GetController(Type controllerType, IServiceScope scope)
        {
            if (scope.ServiceProvider.GetService(controllerType) is not BotControllerBase controllerObj)
            {
                throw new InvalidOperationException("controller is not resolved.");
            }

            return controllerObj;
        }
    }
}

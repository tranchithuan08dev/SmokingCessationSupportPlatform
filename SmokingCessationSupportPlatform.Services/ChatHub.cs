using Microsoft.AspNetCore.SignalR;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }
        public async Task SendMessageToUser(int conversationId, string senderType, string messageContent, int senderId, string receiverType, int receiverId)
        {
            try
            {
                var sentMessageViewModel = await _chatService.SendMessageAsync(
                    conversationId,
                    senderId,
                    senderType,
                    receiverId,
                    receiverType,
                    messageContent
                );


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ChatHub.SendMessageToUser: {ex.Message}");
                throw new HubException($"Failed to send message: {ex.Message}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinChat(string conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
        }

        public async Task LeaveChat(string conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
        }
    }
}

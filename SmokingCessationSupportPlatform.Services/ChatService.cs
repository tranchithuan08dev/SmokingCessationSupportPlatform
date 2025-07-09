using Microsoft.AspNetCore.SignalR;
using SmokingCessationSupportPlatform.BusinessObjects.DTO;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services
{
    public class ChatService : IChatService
    {
        private readonly IConservationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(
            IConservationRepository conversationRepository,
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            ICoachRepository coachRepository,
            IHubContext<ChatHub> hubContext)
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _coachRepository = coachRepository;
            _hubContext = hubContext;
        }

       public async Task<Conversation> GetOrCreateConversationAsync(int userId, int coachId)
        {
            var conversation = await _conversationRepository.GetConversationByParticipantsAsync(userId, coachId);
            if (conversation == null)
            {
                conversation = new Conversation
                {
                    UserId = userId,
                    CoachId = coachId,
                    StartedAt = DateTime.Now,
                    LastMessageAt = DateTime.Now
                };
                await _conversationRepository.AddConversationAsync(conversation);
            }
            return conversation;
        }

        public async Task<List<Conversation>> GetUserConversationsAsync(int userId)
        {
            return await _conversationRepository.GetUserConversationsAsync(userId);
        }

        public async Task<List<Conversation>> GetCoachConversationsAsync(int coachId)
        {
            return await _conversationRepository.GetCoachConversationsAsync(coachId);
        }

        // --- Message Management ---

        public async Task<MessageViewModel?> SendMessageAsync(int conversationId, int fromId, string fromType, int toId, string toType, string content)
        {
            var message = new Message
            {
                ConversationId = conversationId,
                FromId = fromId,
                FromType = fromType,
                ToId = toId,
                ToType = toType,
                Content = content,
                SentAt = DateTime.Now,
                IsRead = false
            };

            await _messageRepository.AddMessageAsync(message);

            var conversation = await _conversationRepository.GetConversationByIdAsync(conversationId);
            if (conversation != null)
            {
                conversation.LastMessageAt = DateTime.Now;
                await _conversationRepository.UpdateConversationAsync(conversation);
            }

            string fromName = await GetUserNameOrCoachName(fromId, fromType);
            string toName = await GetUserNameOrCoachName(toId, toType);

            var messageViewModel = new MessageViewModel
            {
                MessageId = message.MessageId,
                ConversationId = message.ConversationId,
                FromId = message.FromId,
                FromType = message.FromType,
                ToId = message.ToId,
                ToType = message.ToType,
                Content = message.Content,
                SentAt = message.SentAt,
                IsRead = message.IsRead,
                FromName = fromName,
                ToName = toName
            };

            await _hubContext.Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", messageViewModel);

            return messageViewModel;
        }

        public async Task<List<MessageViewModel>> GetConversationMessagesWithSenderInfoAsync(int conversationId)
        {
            var messages = await _messageRepository.GetConversationMessagesAsync(conversationId);

            var allUserIds = new HashSet<int>();
            var allCoachIds = new HashSet<int>();

            foreach (var message in messages)
            {
                if (message.FromType == "User") allUserIds.Add(message.FromId);
                else if (message.FromType == "Coach") allCoachIds.Add(message.FromId);

                if (message.ToType == "User") allUserIds.Add(message.ToId);
                else if (message.ToType == "Coach") allCoachIds.Add(message.ToId);
            }

            var users = (await _userRepository.GetUsersByIdsAsync(allUserIds)).ToDictionary(u => u.UserId, u => u.Username);
            var coaches = (await _coachRepository.GetCoachesByIdsAsync(allCoachIds)).ToDictionary(c => c.CoachId, c => c.CoachNavigation.Username);

            var messageViewModels = new List<MessageViewModel>();
            foreach (var message in messages)
            {
                string fromName = message.FromType == "User" ? users.GetValueOrDefault(message.FromId, "Người dùng không xác định") : coaches.GetValueOrDefault(message.FromId, "Huấn luyện viên không xác định");
                string toName = message.ToType == "User" ? users.GetValueOrDefault(message.ToId, "Người dùng không xác định") : coaches.GetValueOrDefault(message.ToId, "Huấn luyện viên không xác định");

                messageViewModels.Add(new MessageViewModel
                {
                    MessageId = message.MessageId,
                    ConversationId = message.ConversationId,
                    FromId = message.FromId,
                    FromType = message.FromType,
                    ToId = message.ToId,
                    ToType = message.ToType,
                    Content = message.Content,
                    SentAt = message.SentAt,
                    IsRead = message.IsRead,
                    FromName = fromName,
                    ToName = toName
                });
            }
            return messageViewModels;
        }

        public async Task MarkMessagesAsReadAsync(int conversationId, int receiverId, string receiverType)
        {
            var unreadMessages = await _messageRepository.GetUnreadMessagesInConversationAsync(conversationId, receiverId, receiverType);
            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
                await _messageRepository.UpdateMessageAsync(message);
            }
        }

        private async Task<string> GetUserNameOrCoachName(int id, string type)
        {
            if (type == "User")
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                return user?.Username ?? "Người dùng không xác định";
            }
            else if (type == "Coach")
            {
                var coach = await _coachRepository.GetCoachByIdWithUserInfoAsync(id);
                return coach?.CoachNavigation?.Username ?? "Huấn luyện viên không xác định";
            }
            return "Không xác định";
        }

     
    }
}

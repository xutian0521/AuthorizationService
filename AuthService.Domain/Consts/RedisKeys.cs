﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Domain.Consts
{
    public sealed class RedisKeys
    {
        public static string GetTokenCacheKey(string token)
        {
            return $"Token:{token}";
        }

        public const string User = "User";

        public const string TopicVisit = "TopicVisit";

        public static string GetUserMessageCacheKey(long userID)
        {
            return $"Message:User:{userID}";
        }
    }
}

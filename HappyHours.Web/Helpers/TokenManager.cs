using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyHours.Web.Helpers
{
    public static class TokenManager
    {
        public static ConcurrentDictionary<long, Guid> _tokens = new ConcurrentDictionary<long, Guid>();

        /*public static string CreateNewToken(long userId)
        {
            Guid guid;
            if (!_tokens.TryGetValue(userId, out guid))
            {
                guid = Guid.NewGuid();
                _tokens.TryAdd(userId, guid);
            }
            else
            {
                _tokens[userId] 
            }
        }*/
    }
}
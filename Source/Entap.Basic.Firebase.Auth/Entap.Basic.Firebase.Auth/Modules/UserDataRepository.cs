﻿using System;
using System.Threading.Tasks;
using Entap.Basic.Api;

namespace Entap.Basic.Firebase.Auth
{
    public class UserDataRepository : IUserDataRepository
    {
        IAccessTokenPreferencesService _preferencesService;
        public UserDataRepository(IAccessTokenPreferencesService preferencesService)
        {
            _preferencesService = preferencesService;
        }

        public virtual Task<ServerAccessToken> GetAccessToken()
            => _preferencesService.GetAccessTokenAsync();

        public virtual Task SetAccessTokenAsync(ServerAccessToken accessToken)
            => _preferencesService.SetAccessTokenAsync(accessToken);

        public virtual void RemoveAccessToken()
            => _preferencesService.RemoveAccessToken();


    }
}

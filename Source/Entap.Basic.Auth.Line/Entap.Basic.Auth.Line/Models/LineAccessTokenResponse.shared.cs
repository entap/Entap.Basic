﻿using System;
using Entap.Basic.Auth.OAuth2;
using Newtonsoft.Json;

namespace Entap.Basic.Auth.Line
{
    /// <summary>
    /// アクセストークンレスポンス
    /// https://developers.line.biz/ja/reference/line-login/#issue-token-response
    /// </summary>
    public class LineAccessTokenResponse : AccessTokenResponse
    {
        public LineAccessTokenResponse()
        {
        }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
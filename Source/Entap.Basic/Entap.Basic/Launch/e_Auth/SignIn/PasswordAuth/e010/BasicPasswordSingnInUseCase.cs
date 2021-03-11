﻿using System;
using System.Text.RegularExpressions;

namespace Entap.Basic.Launch.Auth
{
    public class BasicPasswordSingnInUseCase : IPasswordSignInUseCase
    {
        // ToDo : BasicSignUpUseCaseと共有化
        public static readonly string RegexMailAddress = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        public static readonly string RegexPassword = @"^[!-~]*$";

        public BasicPasswordSingnInUseCase()
        {
        }

        public string ValidateMailAddress(string mailAddress)
        {
            if (string.IsNullOrEmpty(mailAddress)) return null;
            if (!Regex.IsMatch(mailAddress, RegexMailAddress)) return "形式不正";
            return null;
        }

        public virtual string ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;
            if (password.Length < 8) return "桁数不足";
            if (!Regex.IsMatch(password, RegexPassword)) return "形式不正";

            return null;
        }

        public void SignIn(string mailAddress, string password)
        {
            throw new NotImplementedException();
        }

        public void ResetPassword()
        {
            throw new NotImplementedException();
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
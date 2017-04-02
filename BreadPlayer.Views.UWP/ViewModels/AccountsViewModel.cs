﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreadPlayer.Web.Lastfm;
using BreadPlayer.Common;
using System.Windows.Input;

namespace BreadPlayer.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        #region Lastfm Configuration
        DelegateCommand lastfmLoginCommand;
        string lastfmUsername;
        string lastfmPassword;
        public string LastfmUsername
        {
            get { return lastfmUsername; }
            set
            {
                Set(ref lastfmUsername, value);
                RoamingSettingsHelper.SaveSetting("LastfmUsername", value);
            }
        }
        public string LastfmPassword
        {
            get { return lastfmPassword; }
            set
            {
                Set(ref lastfmPassword, value);
                RoamingSettingsHelper.SaveSetting("LastfmPassword", value);
            }
        }
        public ICommand LastfmLoginCommand
        {
            get { if(lastfmLoginCommand == null) lastfmLoginCommand = new DelegateCommand(LastfmLogin); return lastfmLoginCommand; }
        }
        private void LastfmLogin()
        {
            if (!LastfmPassword.Any() || !LastfmUsername.Any())
                return;
            InitializeLastfm lastfm = new InitializeLastfm(LastfmUsername, LastfmPassword);
            LastfmScrobbler = new Lastfm(lastfm.Auth.Auth);
        }
        #endregion

        public AccountsViewModel()
        {
            LastfmPassword = RoamingSettingsHelper.GetSetting<string>("LastfmPassword", "");
            LastfmUsername = RoamingSettingsHelper.GetSetting<string>("LastfmUsername", "");
            LastfmLogin();
        }
    }
}
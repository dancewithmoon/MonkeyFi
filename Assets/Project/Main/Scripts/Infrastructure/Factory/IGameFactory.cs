﻿using System.Threading.Tasks;
using Services.Leaderboard;
using Services.Referral;
using Services.TonWallet;
using UI;
using UI.Elements;
using UI.Windows;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        void CreateUIRoot();
        public BaseWindow CreateWindow(WindowType windowType);
        Task<HudOverlay> CreateHudOverlay();
        Task<LeaderboardItem> CreateLeaderboardItem(LeaderboardEntryModel model, Transform parent);
        Task<FriendItem> CreateFriendItem(ReferralModel model, Transform parent);
        Task<WalletItem> CreateWalletItem(WalletModel model, Transform parent);
    }
}
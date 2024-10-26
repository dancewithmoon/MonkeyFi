using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Referral
{
    public interface IReferralService
    {
        List<ReferralModel> Referrals { get; }
        void ConnectToReferrer(string referralCode);
        Task LoadReferrals();
        void InviteFriends();
    }
}
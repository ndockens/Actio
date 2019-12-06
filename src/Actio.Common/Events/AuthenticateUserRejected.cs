namespace Actio.Common.Events
{
    public class AuthenticateUserRejected : IRejectedEvent
    {
        public string Reason { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }

        protected AuthenticateUserRejected()
        {

        }

        public AuthenticateUserRejected(string reason, string code, string email)
        {
            Reason = reason;
            Code = code;
            Email = email;
        }
    }
}
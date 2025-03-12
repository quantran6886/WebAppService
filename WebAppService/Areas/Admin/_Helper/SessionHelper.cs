using Newtonsoft.Json;
using WebAppService.Models;

namespace WebAppService.Areas.Admin._Helper
{
    public class SessionHelper
    {
        public static void SetUser(ISession session, ViewUserOnline user)
        {
            session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
        }

        public static ViewUserOnline GetUser(ISession session)
        {
            string userJson = session.GetString("CurrentUser");
            if (!string.IsNullOrEmpty(userJson))
            {
                return JsonConvert.DeserializeObject<ViewUserOnline>(userJson);
            }
            return null;
        }
    }
}

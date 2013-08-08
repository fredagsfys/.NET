using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Easyfy.CHS.Model.Athlete;
using System.Threading.Tasks;

namespace Easyfy.CHS
{
    public class AthleteHub : Hub
    {
        public void Send(string id, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.Group(id).addNewMessageToPage(id, message);

        }

        public void SendWodResult(dynamic data)
        {
            string fullName = Convert.ToString(data.fullName);
            string userName = Convert.ToString(data.userName);
            string wodName = Convert.ToString(data.wodName);
            string result = Convert.ToString(data.result);
            string resultType = Convert.ToString(data.resultType);
            string date = Convert.ToString(data.date);
            string Id = Convert.ToString(data.Id);
            // Call the addNewMessageToPage method to update clients.
            Clients.Group(Id).message(Id, fullName, userName, wodName, result, resultType, date);

        }

        public Task StartFollowAthlete(string id)
        {
            return Groups.Add(Context.ConnectionId, id);
        }
        public Task StopFollowPerson(string id)
        {
            return Groups.Remove(Context.ConnectionId, id);
        }
    }
}
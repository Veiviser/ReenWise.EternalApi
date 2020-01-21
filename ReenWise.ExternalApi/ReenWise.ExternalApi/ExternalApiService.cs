using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ReenWise.ExternalApi.Authentication;
using ReenWise.ExternalApi.Models;

namespace ReenWise.ExternalApi
{
    partial class ExternalApiService : ServiceBase
    {
        private AccessToken token;
        public ExternalApiService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            token = AuthenticateToken.GetToken();

            Timer timer = new Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.


            List<DataTransferObject> response = Get.GetData(token.access_token);
            Post.PostData(response);
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}

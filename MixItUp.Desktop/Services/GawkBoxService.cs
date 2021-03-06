﻿using Mixer.Base.Clients;
using Mixer.Base.Model.OAuth;
using MixItUp.Base.Commands;
using MixItUp.Base.Model.User;
using MixItUp.Base.Services;
using MixItUp.Base.Util;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MixItUp.Desktop.Services
{
    public class GawkBoxWebSocketClient : WebSocketClientBase
    {
        private IGawkBoxService service;

        public GawkBoxWebSocketClient(IGawkBoxService service) { this.service = service; }

        protected override async Task ProcessReceivedPacket(string packetJSON)
        {
            if (!string.IsNullOrEmpty(packetJSON))
            {
                GawkBoxAlert alert = JsonConvert.DeserializeObject<GawkBoxAlert>(packetJSON);
                if (alert != null && alert.Gifts.Count > 0)
                {
                    UserDonationModel donation = alert.ToGenericDonation();
                    await EventCommand.ProcessDonationEventCommand(donation, OtherEventTypeEnum.GawkBoxDonation);
                }
            }
        }
    }

    public class GawkBoxService : IGawkBoxService
    {
        private OAuthTokenModel token;
        private GawkBoxWebSocketClient webSocket;

        public GawkBoxService(string gawkBoxID) : this(new OAuthTokenModel() { accessToken = gawkBoxID }) { }

        public GawkBoxService(OAuthTokenModel token) { this.token = token; }

        public async Task<bool> Connect()
        {
            try
            {
                this.webSocket = new GawkBoxWebSocketClient(this);
                if (await this.webSocket.Connect("wss://stream.gawkbox.com/ws/" + this.token.accessToken))
                {
                    GlobalEvents.ServiceReconnect("GawkBox");

                    this.webSocket.OnDisconnectOccurred += WebSocket_OnDisconnectOccurred;
                    return true;
                }
            }
            catch (Exception ex) { MixItUp.Base.Util.Logger.Log(ex); }
            return false;
        }

        private async void WebSocket_OnDisconnectOccurred(object sender, System.Net.WebSockets.WebSocketCloseStatus e)
        {
            GlobalEvents.ServiceDisconnect("GawkBox");

            do
            {
                await Task.Delay(2500);
            }
            while (!await this.Connect());

            GlobalEvents.ServiceReconnect("GawkBox");
        }

        public async Task Disconnect()
        {
            if (this.webSocket != null)
            {
                this.webSocket.OnDisconnectOccurred -= WebSocket_OnDisconnectOccurred;
                await this.webSocket.Disconnect();
            }
        }

        public OAuthTokenModel GetOAuthTokenCopy() { return this.token; }
    }
}

using BeetleX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace TcpServer
{
    public class TcpInit
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        private TcpInit()
        {
        }

        #endregion

        #region Member

        /// <summary>
        /// bu classın nesnesi
        /// </summary>
        private static TcpInit tcpInit;

        #endregion

        #region Properties

        /// <summary>
        /// server interfacesi
        /// </summary>
        private IServer server { get; set; }

        #endregion

        #region Event

        public event EventHandler<byte[]> DataReceived;
        public event EventHandler DeviceConnected;
        public event EventHandler DeviceDisconnected;

        #endregion

        #region Method

        /// <summary>
        /// tcp getinstance meetodu clası buradan dışarı açıyoruz(Constructor burdan tetikleniyor)
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static TcpInit GetIntance()
        {
            if (tcpInit == null)
                tcpInit = new TcpInit();
            return tcpInit;
        }

        /// <summary>
        /// Server oluşturup envorimentten aldığı portu dinlemeye başlar
        /// </summary>
        /// <returns></returns>
        public IServer GetServer()
        {
            if (server == null)
            {
                server = SocketFactory.CreateTcpServer<TcpConsumer>(new ServerOptions().AddListen(IPAddress.Any.ToString(),
                    int.Parse(Environment.GetEnvironmentVariable("TCP_PORT"))));
            }
            return server;
        }

        /// <summary>
        /// tcp serverı başlatır
        /// </summary>
        public void StartConsume()
        {
            try
            {
                if (!GetServer().Open())
                    GetServer().Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex + "TcpConsumer/StartConsume"}");
            }
        }

        /// <summary>
        /// tcp serveri durdurur
        /// </summary>
        public void StopConsume()
        {
            try
            {
                if (GetServer().Open())
                    GetServer().Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex + "TcpConsumer/StopConsume"}");
            }
        }

        /// <summary>
        /// Data düşünce tetiklenecek event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TcpDataReceived(object sender, byte[] e)
        {
            DataReceived?.Invoke(sender, e);
        }

        #endregion
    }
}

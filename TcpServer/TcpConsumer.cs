using BeetleX;
using BeetleX.EventArgs;
using Newtonsoft.Json;
using System;
using System.IO;

namespace TcpServer
{
    public class TcpConsumer : ServerHandlerBase
    {
        #region Constructor

        public TcpConsumer()
        {
        }

        #endregion

        #region Method

        /// <summary>
        /// düşen datayı yakalayacan metot
        /// </summary>
        /// <param name="server"></param>
        /// <param name="e"></param>
        public override void SessionReceive(IServer server, SessionReceiveEventArgs e)
        {
            try
            {
                TcpResponseModel tcpResponseModel = new TcpResponseModel()
                {
                    Result = "OK"
                };
                BeetleX.Buffers.PipeStream pipeStream = e.Stream.ToPipeStream();
                using (MemoryStream memoryStream = new MemoryStream(pipeStream.FirstBuffer.Data)) //pipeStream1.LastBuffer.Data
                using (StreamReader reader = new StreamReader(memoryStream))
                {
                    var data = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(data))
                        TcpInit.GetIntance().TcpDataReceived(data, null);

                    pipeStream.Write(@"{""result"":""OK""}!");
                    e.Session.Stream.Flush();
                    base.SessionReceive(server, e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex + "TcpConsumer/SessionReceive"}");
            }
        }

        /// <summary>
        /// device connect olduğunda çalışan metot
        /// </summary>
        /// <param name="server"></param>
        /// <param name="e"></param>
        public override void Connected(IServer server, ConnectedEventArgs e)
        {
            Console.WriteLine("Tcp Device Connection ID : " + e.Session.ID);
        }

        /// <summary>
        /// Device disconnect olduğunda çalışan metot
        /// </summary>
        /// <param name="server"></param>
        /// <param name="e"></param>
        public override void Disconnect(IServer server, SessionEventArgs e)
        {
            Console.WriteLine("Tcp Device Disconnection ID : " + e.Session.ID);
        }

        /// <summary>
        /// TCP hatalarını yakalar
        /// </summary>
        /// <param name="server"></param>
        /// <param name="e"></param>
        public override void Error(IServer server, ServerErrorEventArgs e)
        {
            Console.WriteLine("Tcp Device Error : " + e.Error.Message);
        }

        /// <summary>
        /// Tcp loglarını yazar
        /// </summary>
        /// <param name="server"></param>
        /// <param name="e"></param>
        public override void Log(IServer server, ServerLogEventArgs e)
        {
            Console.WriteLine("Tcp Log :" + e.Message);
        }

        #endregion
    }
}

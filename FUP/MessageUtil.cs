using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUP
{
    public class MessageUtil
    {
        public static void Send(Stream writer, Message msg)
        {
            writer.Write(msg.GetBytes(), 0, msg.GetSize());
        }

        public static Message Receive(Stream reader)
        {
            int totalRecv = 0;
            int sizeToRead = 16;
            byte[] hbuffer = new byte[sizeToRead];

            while (sizeToRead > 0)
            {
                byte[] buffer = new byte[sizeToRead];
                int recv = reader.Read(buffer, 0, sizeToRead);
                if (recv == 0)
                {
                    return null;
                }

                buffer.CopyTo(hbuffer, totalRecv);
                totalRecv += recv;
                sizeToRead -= recv;
            }

            ISerializable body = null;
            switch (Header.MSGTYPE)
            {
                case CONSTANTS.REQ_FILE_SEND:
                    body = new BodyRequest(bBuffer);
                    break;
                case CONSTANTS.REP_FILE_SEND:
                    body = new BodyResponse(bBuffer);
                    break;
                case CONSTANTS.FILE_SEND_DATA:
                    body = new BodyData(bBuffer);
                    break;
                default:
                    throw new Exception(
                        String.Format(
                            "Unknown MSGTYPE : {0}", Header.MSGTYPE));
            }

            return new Message() { Header = Header, Body = body };
        }
    }
}
